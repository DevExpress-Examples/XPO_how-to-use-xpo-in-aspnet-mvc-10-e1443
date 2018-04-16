Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Security
Imports System.Web.UI

Namespace MvcGridView.Controllers

	<HandleError> _
	Public Class AccountController
		Inherits Controller

		' This constructor is used by the MVC framework to instantiate the controller using
		' the default forms authentication and membership providers.

		Public Sub New()
			Me.New(Nothing, Nothing)
		End Sub

		' This constructor is not used by the MVC framework but is instead provided for ease
		' of unit testing this type. See the comments at the end of this file for more
		' information.
		Public Sub New(ByVal formsAuth As IFormsAuthentication, ByVal service As IMembershipService)
			FormsAuth = If(formsAuth, New FormsAuthenticationService())
			MembershipService = If(service, New AccountMembershipService())
		End Sub

		Private privateFormsAuth As IFormsAuthentication
		Public Property FormsAuth() As IFormsAuthentication
			Get
				Return privateFormsAuth
			End Get
			Private Set(ByVal value As IFormsAuthentication)
				privateFormsAuth = value
			End Set
		End Property

		Private privateMembershipService As IMembershipService
		Public Property MembershipService() As IMembershipService
			Get
				Return privateMembershipService
			End Get
			Private Set(ByVal value As IMembershipService)
				privateMembershipService = value
			End Set
		End Property

		Public Function LogOn() As ActionResult

			Return View()
		End Function

		<AcceptVerbs(HttpVerbs.Post), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification := "Needs to take same parameter type as Controller.Redirect()")> _
		Public Function LogOn(ByVal userName As String, ByVal password As String, ByVal rememberMe As Boolean, ByVal returnUrl As String) As ActionResult

			If (Not ValidateLogOn(userName, password)) Then
				Return View()
			End If

			FormsAuth.SignIn(userName, rememberMe)
			If (Not String.IsNullOrEmpty(returnUrl)) Then
				Return Redirect(returnUrl)
			Else
				Return RedirectToAction("Index", "Home")
			End If
		End Function

		Public Function LogOff() As ActionResult

			FormsAuth.SignOut()

			Return RedirectToAction("Index", "Home")
		End Function

		Public Function Register() As ActionResult

			ViewData("PasswordLength") = MembershipService.MinPasswordLength

			Return View()
		End Function

		<AcceptVerbs(HttpVerbs.Post)> _
		Public Function Register(ByVal userName As String, ByVal email As String, ByVal password As String, ByVal confirmPassword As String) As ActionResult

			ViewData("PasswordLength") = MembershipService.MinPasswordLength

			If ValidateRegistration(userName, email, password, confirmPassword) Then
				' Attempt to register the user
				Dim createStatus As MembershipCreateStatus = MembershipService.CreateUser(userName, password, email)

				If createStatus = MembershipCreateStatus.Success Then
'INSTANT VB NOTE: Embedded comments are not maintained by Instant VB
'ORIGINAL LINE: FormsAuth.SignIn(userName, false /* createPersistentCookie */);
					FormsAuth.SignIn(userName, False)
					Return RedirectToAction("Index", "Home")
				Else
					ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus))
				End If
			End If

			' If we got this far, something failed, redisplay form
			Return View()
		End Function

		<Authorize> _
		Public Function ChangePassword() As ActionResult

			ViewData("PasswordLength") = MembershipService.MinPasswordLength

			Return View()
		End Function

		<Authorize, AcceptVerbs(HttpVerbs.Post), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification := "Exceptions result in password not being changed.")> _
		Public Function ChangePassword(ByVal currentPassword As String, ByVal newPassword As String, ByVal confirmPassword As String) As ActionResult

			ViewData("PasswordLength") = MembershipService.MinPasswordLength

			If (Not ValidateChangePassword(currentPassword, newPassword, confirmPassword)) Then
				Return View()
			End If

			Try
				If MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword) Then
					Return RedirectToAction("ChangePasswordSuccess")
				Else
					ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.")
					Return View()
				End If
			Catch
				ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.")
				Return View()
			End Try
		End Function

		Public Function ChangePasswordSuccess() As ActionResult

			Return View()
		End Function

		Protected Overrides Sub OnActionExecuting(ByVal filterContext As ActionExecutingContext)
			If TypeOf filterContext.HttpContext.User.Identity Is WindowsIdentity Then
				Throw New InvalidOperationException("Windows authentication is not supported.")
			End If
		End Sub

		#Region "Validation Methods"

		Private Function ValidateChangePassword(ByVal currentPassword As String, ByVal newPassword As String, ByVal confirmPassword As String) As Boolean
			If String.IsNullOrEmpty(currentPassword) Then
				ModelState.AddModelError("currentPassword", "You must specify a current password.")
			End If
			If newPassword Is Nothing OrElse newPassword.Length < MembershipService.MinPasswordLength Then
				ModelState.AddModelError("newPassword", String.Format(CultureInfo.CurrentCulture, "You must specify a new password of {0} or more characters.", MembershipService.MinPasswordLength))
			End If

			If (Not String.Equals(newPassword, confirmPassword, StringComparison.Ordinal)) Then
				ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.")
			End If

			Return ModelState.IsValid
		End Function

		Private Function ValidateLogOn(ByVal userName As String, ByVal password As String) As Boolean
			If String.IsNullOrEmpty(userName) Then
				ModelState.AddModelError("username", "You must specify a username.")
			End If
			If String.IsNullOrEmpty(password) Then
				ModelState.AddModelError("password", "You must specify a password.")
			End If
			If (Not MembershipService.ValidateUser(userName, password)) Then
				ModelState.AddModelError("_FORM", "The username or password provided is incorrect.")
			End If

			Return ModelState.IsValid
		End Function

		Private Function ValidateRegistration(ByVal userName As String, ByVal email As String, ByVal password As String, ByVal confirmPassword As String) As Boolean
			If String.IsNullOrEmpty(userName) Then
				ModelState.AddModelError("username", "You must specify a username.")
			End If
			If String.IsNullOrEmpty(email) Then
				ModelState.AddModelError("email", "You must specify an email address.")
			End If
			If password Is Nothing OrElse password.Length < MembershipService.MinPasswordLength Then
				ModelState.AddModelError("password", String.Format(CultureInfo.CurrentCulture, "You must specify a password of {0} or more characters.", MembershipService.MinPasswordLength))
			End If
			If (Not String.Equals(password, confirmPassword, StringComparison.Ordinal)) Then
				ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.")
			End If
			Return ModelState.IsValid
		End Function

		Private Shared Function ErrorCodeToString(ByVal createStatus As MembershipCreateStatus) As String
			' See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
			' a full list of status codes.
			Select Case createStatus
				Case MembershipCreateStatus.DuplicateUserName
					Return "Username already exists. Please enter a different user name."

				Case MembershipCreateStatus.DuplicateEmail
					Return "A username for that e-mail address already exists. Please enter a different e-mail address."

				Case MembershipCreateStatus.InvalidPassword
					Return "The password provided is invalid. Please enter a valid password value."

				Case MembershipCreateStatus.InvalidEmail
					Return "The e-mail address provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.InvalidAnswer
					Return "The password retrieval answer provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.InvalidQuestion
					Return "The password retrieval question provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.InvalidUserName
					Return "The user name provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.ProviderError
					Return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator."

				Case MembershipCreateStatus.UserRejected
					Return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator."

				Case Else
					Return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator."
			End Select
		End Function
		#End Region
	End Class

	' The FormsAuthentication type is sealed and contains static members, so it is difficult to
	' unit test code that calls its members. The interface and helper class below demonstrate
	' how to create an abstract wrapper around such a type in order to make the AccountController
	' code unit testable.

	Public Interface IFormsAuthentication
		Sub SignIn(ByVal userName As String, ByVal createPersistentCookie As Boolean)
		Sub SignOut()
	End Interface

	Public Class FormsAuthenticationService
		Implements IFormsAuthentication
		Public Sub SignIn(ByVal userName As String, ByVal createPersistentCookie As Boolean) Implements IFormsAuthentication.SignIn
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie)
		End Sub
		Public Sub SignOut() Implements IFormsAuthentication.SignOut
			FormsAuthentication.SignOut()
		End Sub
	End Class

	Public Interface IMembershipService
		ReadOnly Property MinPasswordLength() As Integer

		Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean
		Function CreateUser(ByVal userName As String, ByVal password As String, ByVal email As String) As MembershipCreateStatus
		Function ChangePassword(ByVal userName As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean
	End Interface

	Public Class AccountMembershipService
		Implements IMembershipService
		Private _provider As MembershipProvider

		Public Sub New()
			Me.New(Nothing)
		End Sub

		Public Sub New(ByVal provider As MembershipProvider)
			_provider = If(provider, Membership.Provider)
		End Sub

		Public ReadOnly Property MinPasswordLength() As Integer Implements IMembershipService.MinPasswordLength
			Get
				Return _provider.MinRequiredPasswordLength
			End Get
		End Property

		Public Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean Implements IMembershipService.ValidateUser
			Return _provider.ValidateUser(userName, password)
		End Function

		Public Function CreateUser(ByVal userName As String, ByVal password As String, ByVal email As String) As MembershipCreateStatus Implements IMembershipService.CreateUser
			Dim status As MembershipCreateStatus
			_provider.CreateUser(userName, password, email, Nothing, Nothing, True, Nothing, status)
			Return status
		End Function

		Public Function ChangePassword(ByVal userName As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean Implements IMembershipService.ChangePassword
'INSTANT VB NOTE: Embedded comments are not maintained by Instant VB
'ORIGINAL LINE: MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
			Dim currentUser As MembershipUser = _provider.GetUser(userName, True)
			Return currentUser.ChangePassword(oldPassword, newPassword)
		End Function
	End Class
End Namespace
