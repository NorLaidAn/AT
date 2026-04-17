Feature: EHU Website

  Scenario: User opens About page
    Given user is on EHU homepage
    When user clicks About tab
    Then About page should be opened

  Scenario: User searches for study programs
    Given user is on EHU homepage
    When user searches for "study programs"
    Then search results page should be opened

  Scenario: User changes language
    Given user is on EHU homepage
    When user changes language to "lt"
    Then Lithuanian version should be displayed

  Scenario: User opens contacts
    Given user is on EHU homepage
    When user opens contacts page
    Then contact information should be visible