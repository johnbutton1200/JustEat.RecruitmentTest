﻿Feature: Get Restaurants
	So that I can order food
	As a hungry customer
	I want to be able to find restaurants in my area

Scenario: Validate Address schema
	Given I have a restaurants API
	When I request the restaurants by postcode 'BS5 7JW'
	Then the response status code is 'OK'
	And the Address schema should be correct for each restaurant

Scenario: Validate Address field values
	Given I have a restaurants API
	When I request the restaurants by postcode 'BS5 7JW'
	Then the response status code is 'OK'
	And all Address field values are valid for each restaurant

Scenario: Request with invalid postcode string
	Given I have a restaurants API
	When I request the restaurants by postcode 'BS%&65'
	Then the response status code is 'BadRequest'

Scenario: Validate DeliveryEtaMinutes schema
	Given I have a restaurants API
	When I request the restaurants by postcode 'BS5 7JW'
	Then the response status code is 'OK'
	And the DeliveryEtaMinutes schema should be correct for each restaurant