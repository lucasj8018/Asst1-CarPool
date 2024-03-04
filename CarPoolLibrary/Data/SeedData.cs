using CarPoolLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarPoolLibrary.Data;

public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var pwd = "P@$$w0rd";
        var passwordHasher = new PasswordHasher<User>();

        var adminRole = new Role("Admin");
        adminRole.NormalizedName = adminRole.Name!.ToUpper();
        adminRole.Description = "Administrator Role";

        var ownerRole = new Role("Owner");
        ownerRole.NormalizedName = ownerRole.Name!.ToUpper();
        ownerRole.Description = "Owner Role";

        var passengerRole = new Role("Passenger");
        passengerRole.NormalizedName = passengerRole.Name!.ToUpper();
        passengerRole.Description = "Passenger Role";

        List<Role> roles = new List<Role>(){
            adminRole,
            ownerRole,
            passengerRole
        };

        modelBuilder.Entity<Role>().HasData(roles);

        var adminUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "a@a.a",
            Email = "a@a.a",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "Admin"
        };
        adminUser.NormalizedUserName = adminUser.Email.ToUpper();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, pwd);

        var ownerUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "o@o.o",
            Email = "o@o.o",
            EmailConfirmed = true,
            FirstName = "Owner",
            LastName = "Owner"
        };
        ownerUser.NormalizedUserName = ownerUser.Email.ToUpper();
        ownerUser.PasswordHash = passwordHasher.HashPassword(ownerUser, pwd);

        var passengerUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "p@p.p",
            Email = "p@p.p",
            EmailConfirmed = true,
            FirstName = "Passenger",
            LastName = "Passenger"
        };
        passengerUser.NormalizedUserName = passengerUser.Email.ToUpper();
        passengerUser.PasswordHash = passwordHasher.HashPassword(passengerUser, pwd);

        var passengerUserSam = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "sam@fox.com",
            Email = "sam@fox.com",
            EmailConfirmed = true,
            FirstName = "Sam",
            LastName = "Fox"
        };
        passengerUserSam.NormalizedUserName = passengerUserSam.Email.ToUpper();
        passengerUserSam.PasswordHash = passwordHasher.HashPassword(passengerUserSam, pwd);

        var passengerUserAnn = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "ann@day.com",
            Email = "ann@day.com",
            EmailConfirmed = true,
            FirstName = "Ann",
            LastName = "Day"
        };
        passengerUserSam.NormalizedUserName = passengerUserSam.Email.ToUpper();
        passengerUserSam.PasswordHash = passwordHasher.HashPassword(passengerUserSam, pwd);

        var passengerUserLucas = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "lucas@jian.com",
            Email = "lucas@jian.com",
            EmailConfirmed = true,
            FirstName = "Lucas",
            LastName = "Jian"
        };

        List<User> users = new List<User>(){
            adminUser,
            ownerUser,
            passengerUser,
            passengerUserSam,
            passengerUserAnn,
            passengerUserLucas
        };

        modelBuilder.Entity<User>().HasData(users);

        List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = users[0].Id,
            RoleId = roles.First(q => q.Name == "Admin").Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = users[1].Id,
            RoleId = roles.First(q => q.Name == "Owner").Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = users[2].Id,
            RoleId = roles.First(q => q.Name == "Passenger").Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = users[3].Id,
            RoleId = roles.First(q => q.Name == "Passenger").Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = users[4].Id,
            RoleId = roles.First(q => q.Name == "Passenger").Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = users[5].Id,
            RoleId = roles.First(q => q.Name == "Passenger").Id
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);


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
                Country="Canada",
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
                Country="Canada",
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
                Country="Canada",
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