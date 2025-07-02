# LearnLink_Backend

## Intro

The application is created to ease the communication between students and instructors.

It allows instructors to create courses and manage content while offering a scheduling system for meetings.

It allows admins to create posts and threads for users and instructors to interact with.

## Installation 

clone the repo using 
```bash
git clone https://github.com/EbrahimMo50/LearnLink_Backend.git
```
run **dotnet restore** to reload the nugget packags

adjust the database configuration in **appsettings.json** (sqlserver provider)

build the project using **dotnet build**

the application is configured to run with swagger displaying the endpoints on run

## Status

tests are being implemented to prepare for publishing

## Considerations

file structure is not in the best shape. seprating the layers to infrastructure, core and presentation is recommended

user tables follow table per concerete denying scalabillity since there are 3 tables for users; consider following table per type design

testing the repo layer in the application is done in near-unit(in-memory database) testing manner to avoid complex mocking of IAsyncQueryProvider and extensive needless mocking and logic

custom policy authorization is defined and added to the services builder then are called losley-typed through Authorization("policy") annotation on endpoints consider making new policy providers and using them directly as annotations

controllers have httpcontext from their base controller no need to inject the IHttpContext

The application follows REST Api guidlines except applying the (Allow filtering, sorting, and pagination) on all possibly applicable GET endpoints and caching

AutoMapper please
