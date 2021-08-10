# Answers to technical questions

## How long did you spend on the technical test?
Approximately 5 hours.

## What would you add to your solution if you had more time? If you didn't spend much time on the technical test then use this as an opportunity to explain what you would add.
I could have added the ability to run the tests in parallel using the NUnit functionality for this. It's less important when you have only a couple of feature files but can help a lot with test run times when the number of tests increases. 
There are other response status codes I noticed such as `NotFound` which would be easy to implement a test for using the pre-existing steps.

## What do you think is the most interesting trend in test automation?
Using AI based visual regression testing (e.g Applitools) is an area that would be interesting to explore further. This could potentially help to reduce the amount of manual input required from visual regression testing that I have experienced in the past and could help with creating a really scalable and flexible solution.

## How would you implement test automation in a legacy application? Have you ever had to do this?
I have done this before and I believe the key point is to focus on the areas of testing that will bring the most value taking into account the cost/effort. I would find out what the has the most impact to the business if there are issues and concentrate on writing tests for those areas, rather than trying to automate as much as possible in an application that may get superceeded/decomissioned in the not too distant future. Are there any quick wins? Where possible you could try to create the automation solution in such a way that it could be repurposed for a future updated solution.

## How would you improve the customer experience of the Just Eat website?
- Ability to sort/filter by delivery time estimate.
- See similar restaurants on a restaurant page.
- Restaurant recommendations based on previous orders (this may exist but I haven't noticed it).
- Ability to split payments with others.
- Standardised dietary labelling/iconography for food items such as vegan, vegatarian, gluten free etc. This could even be filtered at a restaurant/menu level.