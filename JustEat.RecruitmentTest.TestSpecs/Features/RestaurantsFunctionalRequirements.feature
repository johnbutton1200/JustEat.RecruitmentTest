@singleRequest
Feature: Get Resturants Functional Requirements

Scenario: All restaurants with more than 1 rating should have a star rating greater than 0
	Given I have performed a valid Get Restaurants request
	Then the response status code is 'OK'
	And all restaurants with more than 1 rating should have a star rating greater than 0

Scenario: All the restaurants with no ratings should have a star rating of 0
	Given I have performed a valid Get Restaurants request
	Then the response status code is 'OK'
	And all the restaurants with no ratings should have a star rating of 0

Scenario: 1 restaurant should have a valid URL
	Given I have performed a valid Get Restaurants request
	Then the response status code is 'OK'
	And the first restaurant returned will have a valid URL