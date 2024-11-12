Feature: User Management
  As an administrator
  I want to manage users and ensure that deleting a user does not affect existing orders

  Scenario: Deleting a user does not delete associated orders
    Given a user with ID 900 exists
    And a product with ID 500 exists
    And an order with ID 1000 exists linking user 900 and product 500
    When I delete the user with ID 900
    Then the order with ID 1000 should still exist
