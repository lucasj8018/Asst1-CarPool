using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPoolLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolMvc.Data;

public static class SeedData
{
    // this is an extension method to the ModelBuilder class
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasData(
            GetMembers()
        );

        modelBuilder.Entity<Vehicle>().HasData(
            GetVehicles()
        );

        modelBuilder.Entity<Trip>().HasData(
            GetTrips()
        );

        modelBuilder.Entity<Manifest>().HasData(
            GetManifests()
        );
    }

    public static List<Member> GetMembers()
    {
        List<Member> members = new List<Member>() {
            new Member() {
                MemberId=1,
                FirstName="Sam",
                LastName="Fox",
                Email="sam@fox.com",
                Mobile="778-111-2222",
                Street="457 Fox Avenue",
                City="Richmond",
                PostalCode="V4F 1M7",
                Country="Canada"
            },
            new Member() {    // 1
                MemberId=2,
                FirstName="Ann",
                LastName="Day",
                Email="ann@day.com",
                Mobile="604-333-6666",
                Street="231 Reiver Road",
                City="Delta",
                PostalCode="V6G 1M6",
                Country="Canada"
            },
                new Member() {    // 1
                MemberId=3,
                FirstName="Lucas",
                LastName="Jian",
                Email="lucas@jian.com",
                Mobile="604-333-6666",
                Street="231 Reiver Road",
                City="Delta",
                PostalCode="V6G 1M6",
                Country="Canada"
            },
        };

        return members;
    }

    public static List<Vehicle> GetVehicles()
    {
        List<Vehicle> vehicles = new List<Vehicle>() {
            new Vehicle {
                VehicleId = 1,
                Model = "Escort",
                Make = "Ford",
                Year = 2020,
                NumberOfSeats = 5,
                VehicleType = "Sedan",
                MemberId = 1
            },
            new Vehicle {
                VehicleId = 2,
                Model = "Soul",
                Make = "Kia",
                Year = 2022,
                NumberOfSeats = 5,
                VehicleType = "Compact",
                MemberId = 2
            },
            new Vehicle {
                VehicleId = 3,
                Model = "Odyssey",
                Make = "Honda",
                Year = 2019,
                NumberOfSeats = 8,
                VehicleType = "Minivan",
                MemberId = 3
            }
        };

        return vehicles;
    }

    public static List<Trip> GetTrips()
    {
        List<Trip> trips = new List<Trip>() {
            new Trip {
                TripId = 1,
                VehicleId = 1,
                Date = DateOnly.Parse("2024-02-02"),
                Time = TimeOnly.Parse("12:00"),
                Destination = "123 Marine Drive, Burnaby",
                MeetingAddress = "1123 River Road, Coquitlam"
            },
            new Trip {
                TripId = 2,
                VehicleId = 2,
                Date = DateOnly.Parse("2024-02-03"),
                Time = TimeOnly.Parse("08:00"),
                Destination = "231 Boundary Road, Vancouver",
                MeetingAddress = "345 King George Highway, Surrey"
            },
            new Trip {
                TripId = 3,
                VehicleId = 3,
                Date = DateOnly.Parse("2024-02-04"),
                Time = TimeOnly.Parse("15:00"),
                Destination = "12345 Lougheed Highway, Coquitlam",
                MeetingAddress = "540 Oliver Road, Richmond"
            }
        };

        return trips;
    }

    public static List<Manifest> GetManifests()
    {
        List<Manifest> manifests = new List<Manifest>() {
            new Manifest {
                ManifestId = 1,
                MemberId = 1,
                TripId = 1,
                Notes = "I will be driving to work"
            },
            new Manifest {
                ManifestId = 2,
                MemberId = 2,
                TripId = 2,
                Notes = "I will be driving to work"
            },
            new Manifest {
                ManifestId = 3,
                MemberId = 3,
                TripId = 3,
                Notes = "I will be driving to work"
            }
        };

        return manifests;
    }
}