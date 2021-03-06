﻿using System;
using SalesApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Connectivity;
using SalesApp.Database;
using System.Linq;
using Xamarin.Essentials;

namespace SalesApp.Services.Address
{
    public class MockAddressService : IAddressService
    {
        private readonly ILocalDbService _localDb;
        private DateTime? cacheExpires = null;

        public MockAddressService(ILocalDbService localDb)
        {
            _localDb = localDb;
        }

        public async Task<CurrentLocation> GetAssignedLocationAsync(User user)
        {
            simulateNetworkException();

            await Task.Delay(1000);
            return new CurrentLocation()
            {
                City = "Pittsburg",
                State = "KS",
                Zip = "66762",
                Latitude = 37.405076,
                Longitude = -94.709497,
            };
        }

        public async Task<AddressValidationResult> ValidateAddressAsync(Models.Address address)
        {
            simulateNetworkException();

            await Task.Delay(1000);
            return new AddressValidationResult
            {
                Valid = true,
                Address = address
            };
        }

        public async Task<Models.Address> SaveNewAddressAsync(Models.Address address)
        {
            simulateNetworkException();

            await Task.Delay(1000);

            address.Id = 99;
            return address;
        }

        public async Task<List<Models.Address>> SearchAddressesAsync(string address, string zip)
        {
            simulateNetworkException();

            await Task.Delay(1000);

            List<Models.Address> output = new List<Models.Address>();

            return output;
        }

        public async Task<List<Models.Address>> GetNearbyAddressesAsync(double latitude, double longitude)
        {
            simulateNetworkException();

            List<Models.Address> output = new List<Models.Address>();
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return _localDb.GetAllAddresses();
            }
            else
            {
                if (cacheExpires == null || cacheExpires.Value < DateTime.Now.AddMinutes(-1))
                {
                    cacheExpires = DateTime.Now.AddMinutes(1);
                    output = GetFakeAddresses();

                    foreach (var address in output)
                    {
                        _localDb.UpsertAddress(address);
                    }

                    return output;
                }

                await Task.Delay(2000);

                return _localDb.GetAllAddresses();
            }
            
        }

        private List<Models.Address> GetFakeAddresses()
        {

            return new List<Models.Address>()
            {
                new Models.Address
                {
                    Id=1,
                    City = "Pittsburg",
                    Address1 = "100 South Olive",
                    Province = "KS",
                    PostalCode = "66762",
                    Latitude = 37.405076,
                    Longitude = -94.709497,
                    AddressStatus = new AddressStatus { Description = "Sale", Icon = "ic_money_green.png", Name = "Sale" }
                },
                new Models.Address
                {
                    Id=2,
                    City = "Pittsburg",
                    Address1 = "101 South Olive",
                    Province = "KS",
                    PostalCode = "66762",
                    Latitude = 37.404130,
                    Longitude = -94.709443,
                    AddressStatus = new AddressStatus { Description = "Not Visited", Icon = "ic_info_outline_blue.png", Name = "Not Visited" }
                },
                new Models.Address
                {
                    Id=3,
                    City = "Pittsburg",
                    Address1 = "102 South Olive",
                    Province = "KS",
                    PostalCode = "66762",
                    Latitude = 37.402587,
                    Longitude = -94.709443,
                    AddressStatus = new AddressStatus { Description = "Do Not Knock", Icon = "ic_warning_red.png", Name = "Do Not Knock" }
                },
                new Models.Address
                {
                    Id=4,
                    City = "Pittsburg",
                    Address1 = "101 South College",
                    Province = "KS",
                    PostalCode = "66762",
                    Latitude = 37.403559,
                    Longitude = -94.711476,
                    AddressStatus = new AddressStatus { Description = "Not Visited", Icon = "ic_info_outline_blue.png", Name = "Not Visited" }
                }
            };
        }

        public async Task<Models.Address> GetAddressInfoAsync(long id)
        {
            simulateNetworkException();

            await Task.Delay(1000);

            return new Models.Address
            {
                Id = id,
                City = "Pittsburg",
                Address1 = "100 South Olive",
                Province = "KS",
                PostalCode = "66762",
                Latitude = 37.405076,
                Longitude = -94.709497
            };
        }

        public async Task<AddressBuckets> GetAddressBucketsAsync(string zip)
        {
            List<Models.Address> addresses = new List<Models.Address>();
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                addresses = _localDb.GetAllAddresses();
            }
            else
            {
                if (cacheExpires == null || cacheExpires.Value < DateTime.Now.AddMinutes(-1))
                {
                    cacheExpires = DateTime.Now.AddMinutes(1);
                    addresses = GetFakeAddresses();

                    foreach (var address in addresses)
                    {
                        _localDb.UpsertAddress(address);
                    }


                }
                else
                {
                    addresses = _localDb.GetAllAddresses();
                }
            }
           
            return new AddressBuckets()
            {
                Addresses = addresses.ToList(),               
            };
        }

        public async Task<List<AddressStatus>> GetAddressStatusesAsync()
        {
            simulateNetworkException();

            await Task.Delay(1000);

            List<AddressStatus> output = new List<AddressStatus>();

            AddressStatus status = new AddressStatus
            {
                Id = 1,
                Name = "Do Not Knock",
                Description = "Do Not Knock",
                Icon = ""
            };
            output.Add(status);

            return output;
        }

        public async Task SetAddressStatusAsync(Models.Address address, AddressStatus status)
        {
            simulateNetworkException();

            await Task.Delay(1000);
        }

        public async Task<List<InteractionType>> GetInteractionTypesAsync()
        {
            simulateNetworkException();

            await Task.Delay(1000);

            List<InteractionType> output = new List<InteractionType>();

            InteractionType status = new InteractionType
            {
                Id = 1,
                Name = "No Answer",
                Description = "No Answer",
                Icon = ""
            };
            output.Add(status);

            return output;
        }

        public async Task AddInteractionAsync(UserAddressInteraction userAddressInteraction)
        {
            simulateNetworkException();

            await Task.Delay(1000);
        }


        private static void simulateNetworkException()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new InvalidOperationException("No network connection");
            }
        }
    }
}
