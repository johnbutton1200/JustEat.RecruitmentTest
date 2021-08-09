Feature: Restaurants
	So that I can order food
	As a hungry customer
	I want to be able to find restaurants in my area

Scenario: Validate Restaurants schema
	Given I have a restaurants API
	When I request the restaurants by postcode 'BS5 7JW'
	Then the response status code is 'OK'
	And the Address schema should be correct for each restaurant

@singleRequest
Scenario: All restaurants with more than 1 rating should have a star rating greater than 0
	Given I have performed a valid Get Restaurants request
	Then the response status code is 'OK'
	And all restaurants with more than 1 rating should have a star rating greater than 0

@singleRequest
Scenario: All the restaurants with no ratings should have a star rating of 0
	Given I have performed a valid Get Restaurants request
	Then the response status code is 'OK'
	And all the restaurants with no ratings should have a star rating of 0

@singleRequest
Scenario: 1 restaurant should have a valid URL
	Given I have performed a valid Get Restaurants request
	Then the response status code is 'OK'
	And the first restaurant returned will have a valid URL

Scenario: Request with invalid postcode string
	Given I have a restaurants API
	When I request the restaurants by postcode 'BS%&65'
	Then the response status code is 'OK'

Scenario: Validate Address field values
	Given I have a restaurants API
	When I request the restaurants by postcode 'BS5 7JW'
	Then the response status code is 'OK'
	And all Address field values are valid for each restaurant