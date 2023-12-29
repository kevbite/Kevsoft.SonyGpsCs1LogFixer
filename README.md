# Sony GPS CS1 Log Fixer

Simple console application to fix the date issue within the Sony GPS CS1 log files.

## Background

The GPS-CS1 recently (2023) hit a problem whereby log files were getting the wrong dates written to them, this looks like an issue with a overflow on the device and the device is out of support so there's no updates to the firmware.

[https://community.sony.co.uk/t5/general-chat/gps-cs1-malfunction/td-p/3915101](https://community.sony.co.uk/t5/general-chat/gps-cs1-malfunction/td-p/3915101)

## Log Fixer

This application fixes up each `$GPRMC` message within the log file with a given date offset, correcting the date.

### Usage

Simply run the application with the first argument of the source files and the second argument of the destination directory.

```bash
dotnet run C:\source-files C:\destination-files
```