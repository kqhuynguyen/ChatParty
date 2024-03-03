# ChatParty

The purpose of this application is to demonstrate the capability of ASP.NET and SignalR in the chat use case.

## Features

The following features have been implemented:
- Authentication/Authorization through Identity Core.
- Real-time messaging.
- User-to-user text-based communication
- Group text-based communication.
- Central storage of message through SQL server.
- User search, add-to-group, leave group, etc.

## Roadmap
 
The following features are to be implemented in the future:
- More enriching user profile: profile picture, nicknames, default avatars.
- Subscriber model.
- Multilevel channels.
- Chatbots integration.

## Requirements

- SQLServer.
- .NET 6.0

## How to run the application

- After configuring `appsettings.json` with the appriopriate connection string, run `npm install` to install the node modules and start the ASP.NET project.