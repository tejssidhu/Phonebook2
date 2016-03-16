@LogonTest
Feature: Logon
	In order to access Phoneapp
	As a Phoneapp User
	I want to be able to Logon

Background: 
	Given I am on the Phonebook logon page

Scenario Outline: Unauhorised Message
	When I logon as a user with username: '<username>' and password: '<password>'
	Then I am left on the logon page
	And A logon error message of "Unauthorised" is shown

	Examples: 
	|username	|password	|
	|User123	|wrong		|
	|abc		|123		|
	|User123	|			|

Scenario: Login page is displayed correctly
	Then The login page is displayed correctly
	
Scenario: When no user name is entered, an error message is shown and user is not logged in
	When I logon as a user with username: '' and password: '123'
	Then I am left on the logon page
	And A logon error message of "The Username field is required" is shown

Scenario: When no user name or password is entered, an error message is shown and user is not logged in
	When I logon as a user with username: '' and password: ''
	Then I am left on the logon page
	And A logon error message of "The Username field is required" is shown

Scenario: When a user logs on successfully, they are shown their home page
	When I logon as a user with username: 'User123' and password: '123'
	Then The user's home page is displayed