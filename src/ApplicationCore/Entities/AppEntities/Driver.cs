using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Entities.AppEntities
{
    public class Driver : BaseEntity
    {
        public Driver(string userId, string identificationNumber, string identificationSeries, DateTime identityCardCreateDate, string driverLicenceScanPath, string identityCardPhotoPath)
        {
            UserId = userId;
            IdentificationNumber = identificationNumber;
            IdentificationSeries = identificationSeries;
            IdentityCardCreateDate = identityCardCreateDate;
            DriverLicenceScanPath = driverLicenceScanPath;
            IdentityCardPhotoPath = identityCardPhotoPath;
        }
        public string UserId { get;private set; }
        public string IdentificationNumber { get;private set; }
        public string IdentificationSeries { get; private set;}
        public DateTime IdentityCardCreateDate { get; private set;}
        public string DriverLicenceScanPath { get; private set;}
        public string IdentityCardPhotoPath { get; private set;}
        private Car _car;

        public Car Car
        {
            get => _car;
            set
            {
                if (_car is not null)
                {
                    throw new CarExistsException("Car is already added");
                }
                _car = value;
            }
        }

        public double Rating { get; set; }

    }
}