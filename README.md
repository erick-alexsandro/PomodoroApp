# PomodoroApp
This was an assignment to complete the second semester of the Computer Science course.
>**Note**: Make sure you have created your own MongoDB "PomodoroDatabase" collection named "TodoList," and assigned its connection to the appsettings.json in the correct bin folder.

# Overview
PomodoroApp is a free and open-source application that helps users manage their tasks and study sessions using the Pomodoro technique. It includes features for creating, editing, and completing tasks, as well as controlling work and break times.
# PomodoroApp Capabilities

## Task Management

PomodoroApp helps users create, edit, and complete tasks efficiently. It allows users to organize their tasks into manageable lists, ensuring nothing is overlooked. The app’s intuitive interface makes task management simple and effective.

## Time Management

PomodoroApp employs the Pomodoro technique to help users manage their time effectively. The app includes a timer that divides work into intervals, typically 25 minutes, followed by a short break. This method enhances focus and productivity.

### DashboardViewModel

The DashboardViewModel is responsible for the countdown timer functionality during Pomodoro work sessions. It handles timer events, updates the remaining time, and notifies when sessions are completed.

## Break Management

BreakViewModel manages the timer functionality during breaks between Pomodoro work sessions.

## Task Persistence
TodoViewModel manages the user's task list, allowing CRUD (Create, Read, Update, Delete) operations through interaction with a MongoDB database.
 >*Note: An appsettings.json file is required to configure the MongoDB database.*

## Customization

SettingsViewModel controls the application settings, such as Pomodoro timer durations and appearance themes.

# WUI
PomodoroApp provides a simple and effective user interface to help users stay focused. This interface can also be used to manage tasks that will be completed during study sessions.

**Dashboard view page**
![DashboardView](https://media.discordapp.net/attachments/779085525700444170/1253538441568849991/image.png?ex=66763826&is=6674e6a6&hm=9fd00863b00b3fe509b5eaa576634cc186e3ec504184b97424938d4108826aa4&=&format=webp&quality=lossless&width=1042&height=558)
**Break view page**
![BreakViewModel](https://media.discordapp.net/attachments/779085525700444170/1253538696825933965/image.png?ex=66763862&is=6674e6e2&hm=5469e477393d6927ebe629993dff5a11234434bc5e4e27144d7af85c0b857f58&=&format=webp&quality=lossless&width=1043&height=558)
**TodoList view page**
![TodoListViewModel](https://media.discordapp.net/attachments/779085525700444170/1253538902262943804/image.png?ex=66763893&is=6674e713&hm=e45d04eb26daab815b3ac7abbec0f1730a5a2e3a6b65b246ba156cfbf86e9d2d&=&format=webp&quality=lossless&width=1040&height=558)
**Settings view page**
![SettingsViewModel](https://media.discordapp.net/attachments/779085525700444170/1253539071817552002/image.png?ex=667638bc&is=6674e73c&hm=4e71954dfe2715f8e2fd294c295a4425594ee014e702ea27c00cf17032649962&=&format=webp&quality=lossless&width=1043&height=558)

# Getting Started
First, you need to clone this repository to your local machine.

    git clone https://github.com/erick-alexsandro/PomodoroApp.git
After cloning the project, create your Database. It's name should be "PomodoroDatabase."  Inside it, create a "TodoList" collection.

After creating your MongoDB database, assign its connection string to an appsettings.json file, and move it to the bin folder where the executable you're using is located.

It should look something like this:
```bash
{
	"ConnectionStrings": {
	"MongoDBConnectionString": "mongodb+srv://rest-of-the-link"
	}
}
```

## Conclusion

PomodoroApp utilizes these functionalities to provide an efficient task management experience, combining productivity techniques like the Pomodoro method with advanced data persistence functionalities through MongoDB. Each function plays a crucial role in the overall experience with the application, providing users with the necessary tools to enhance their productivity and manage their tasks effectively.
