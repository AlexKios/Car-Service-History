# Vehicle Service History

## Overview  
**Vehicle Service History** is a lightweight, cross-platform mobile app built with Xamarin and SQLite that helps car owners track and plan routine maintenance. Whether you own one car or a fleet, it keeps a clear, chronological log of every oil change, tyre swap, repair or inspection—and reminds you when it’s due.


## Features

- **Multiple Vehicles**  
  - Add, edit or remove any number of cars  
  - Store make, model, year, engine type, tyre type, current mileage, last-oil-change mileage & date  

- **Detailed Service Logs**  
  - For each car, record unlimited “notes” with date, odometer reading & description  
  - Browse entries one-by-one or view all notes in a scrollable list  

- **Automated Reminders**  
  - Oil–change notifications based on either:  
    - Mileage (every +5 000 km beyond the last change)  
    - Time (every 9 months since last change)  
  - Tyre-swap reminders on fixed calendar dates (spring & autumn) if non “all-season” tyres are fitted  

- **Simple, Intuitive UI**  
  - Native Android Activities with XML layouts  
  - Navigation drawer & menu for quick access: Home, Notes, Add Car  

- **Offline-first Storage**  
  - Local SQLite database:  
    - **Cars** table for vehicle records  
    - One **Notes** database per car for its maintenance entries  

## Technologies & Plugins

- **Xamarin.Android** (C#)  
- **SQLite-net** for local data persistence  
- **Plugin.LocalNotification** for cross-platform local reminders  
- **Xamarin.Essentials** for platform utilities  


## Architecture

1. **MainActivity**  
   - Displays selected car’s summary  
   - OnStop() schedules notifications for all cars  
   - Drawer menu → Home / All Notes / Add Car

2. **Add_Car / Update_Car Activities**  
   - Form validation for tyres (“winter”/“summer”/“all season”) and required fields  
   - Inserts/updates `Car` object into `Cars` table  
   - Creates a new `AllNotes` database file for the car

3. **Show_More_Info Activity**  
   - Loads the selected car’s `AllNotes` SQLiteConnection  
   - Cycles through notes with “Previous”/“Next” buttons or shows all in a ScrollView list  
   - Supports add, edit & delete of individual `Note` entries

4. **Choose_Car Activity**  
   - Lists all cars in a TableLayout  
   - “Select” button tags each row to set the `currentCarId`  


## Data Model

```csharp
// Cars table
public class Car {
  [PrimaryKey, AutoIncrement] public int Id { get; set; }
  public string Model { get; set; }
  public string Year { get; set; }
  public string Engine { get; set; }
  public string Tyre { get; set; }
  public string Km { get; set; }           // Current odometer
  public string KmOfOil { get; set; }      // Odometer at last oil change
  public string DateOfOil { get; set; }    // Date of last oil change
}

// Notes table (one file per car)
public class Note {
  [PrimaryKey, AutoIncrement] public int Id { get; set; }
  public string Date { get; set; }
  public string Km { get; set; }
  public string Text { get; set; }
}
```
Getting Started
    Prerequisites
        Visual Studio 2022 with Xamarin workload
        Android SDK (API 21+)
    Clone & Build
    git clone https://github.com/your-username/vehicle-service-history.git
    cd vehicle-service-history
    open VehicleServiceHistory.sln
    Run on Device / Emulator
        Set MainActivity as startup project
        Deploy to Android emulator or physical device
Usage
    First Launch → App prompts you to Add Car
    Fill in vehicle details and tap Save
    Back on Home, tap Show Notes to view or add service entries
    Use navigation drawer to switch cars or add new ones
    You’ll receive reminders for oil and tyre changes even if the app is in background

Screenshots


![image](https://github.com/user-attachments/assets/b74ba2de-1ba0-44cd-86ae-af90b864a906)



![image](https://github.com/user-attachments/assets/ae98f969-0aa3-46da-bc5b-04f69d70d13b)


Future Improvements
    Track fuel consumption & monthly cost analytics
    Cloud sync (e.g., Google Drive, Firebase) for cross-device backup
    Dark mode & multi-language support
    iOS build via Xamarin.iOS
