Feature: Access Products

@order:3
Scenario: Accessing Products as Logged-in User
    Given a logged-in user with a valid JWT token
    And  user is logged
    When the user requests access to the products API endpoint
    Then the response status code should be 200 OK
    And the response contains a non-empty array of products