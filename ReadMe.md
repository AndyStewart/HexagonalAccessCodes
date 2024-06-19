# Hexagonal Accesscodes

This is a project scaffold for creating accesscodes for opening doors on using the access code the access code will not be able to be used again. 
The solution will implement a WebApi using Hexagonal architecture and a sql server.

# Story 1: Creating an Accesscode

As a user of the web api, I want to be able to create and access code.
An access code must have a maximum of 6 character size.
An access code must have an expiry date and start date.
An access code's start date must before it's expiry date.
An access code must be unique.

# Story 2: Searching for access codes

As a user of the web api, I want to be able to Search for an access code by an exact match of access code 
and for all access code valid between 2 dates. 
In both these situations aa list of matched codes will be returned.

# Story 3: Removing an existing accesscode

As a user of the web api, I want to be able to remove an access code
The access code must already exist
The access code should no longer be returned when searching for an access code.