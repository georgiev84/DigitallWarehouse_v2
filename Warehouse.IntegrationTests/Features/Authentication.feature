Feature: User Login

Scenario: Successful Login with Correct Password
    Given a valid login request with correct password
    When the user submits the login request
    Then the response status code should be 200 OK
    And the response should contain a valid JWT token