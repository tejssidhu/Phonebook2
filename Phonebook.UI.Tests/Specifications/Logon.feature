Feature: Logon
	In order to access Phoneapp
	As a Phoneapp User
	I want to be able to Logon

Background: 
	Given I am on the Phonebook logon page

@UiTest
Scenario: Login page is displayed correctly
	Then The login page is displayed correctly

@UiTest
Scenario: When a wrong user name is entered, an error message is shown and user is not logged in
	When I logon as a user with username: "abc" and password: "123"
	Then I am left on the logon page
	And A logon error message of "Unauthorised" is shown

@UiTest
Scenario: When a wrong password is entered, an error message is shown and user is not logged in
	When I logon as a user with username: "User123" and password: "wrong"
	Then I am left on the logon page
	And A logon error message of "Unauthorised" is shown

@UiTest
Scenario: When no password is entered, a validation icon is shown and user is not logged in
	When I logon as a user with username: "User123" and password: ""
	Then I am left on the logon page
	And A logon error message of "Unauthorised" is shown

@UiTest
Scenario: When no password is entered, an error message is shown and user is not logged in
	When I logon as a user with username: "User123" and password: ""
	Then I am left on the logon page
	And A logon error icon is displayed for "Password"

@UiTest
Scenario: When no user name is entered, an error message is shown and user is not logged in
	When I logon as a user with username: "" and password: "123"
	Then I am left on the logon page
	And A logon error message of "The Username field is required" is shown

@UiTest
Scenario: When no user name is entered, a validation icon is shown and user is not logged in
	When I logon as a user with username: "" and password: "123"
	Then I am left on the logon page
	And A logon error icon is displayed for "Username"

@UiTest
Scenario: When no user name or password is entered, an error message is shown and user is not logged in
	When I logon as a user with username: "" and password: ""
	Then I am left on the logon page
	And A logon error message of "The Username field is required" is shown

@UiTest
Scenario: When no user name or password is entered, a validation icons are shown and user is not logged in
	When I logon as a user with username: "" and password: ""
	Then I am left on the logon page
	And A logon error icon is displayed for "Username"
	And A logon error icon is displayed for "Password"

@UiTest
Scenario: When a user logs on successfully, they are shown their home page
	When I logon as a user with username: "User123" and password: "123"
	Then The user's home page is displayed