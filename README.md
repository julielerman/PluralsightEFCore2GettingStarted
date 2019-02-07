# Pluralsight Course: EF Core 2: GettingStarted
## I have updated all of the projects to use  EF Core 2.2
Sample code for my "EF Core 2: Getting Started" course released Feb 2018  


This is the source code for Pluralsight course EF Core 2: Getting Started.  


Most of these changes are just package ref changes in the csproj files.   

The only code changes I made are: 

* in the Module 3 after and Module 4 after, program.cs, where there is a new method to demonstrate a change to batching in EF COre starting with 2.1. The change that came to 2.1 is that by default, batching is only used when there are 4 or more commands to send to the database.
* I have updated the logging syntax used in the DbContext files to reflect the simpler APIs in 2.2. You'll find this code in the BEFORE and AFTER solutions with comments to explain. For more detail on this change, I've written this blog post: http://thedatafarm.com/data-access/logging-in-ef-core-2-2-has-a-simpler-syntax-more-like-asp-net-core/.

You can watch the course at: http://bit.ly/2oBvekc.

If you don't have a subscription, send me a note and I can send you a code for a free 30-day trial.
http://thedatafarm.com/contact/
