﻿using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data
{
    public class MealService : IMealService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public MealService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }        
        public async Task CreateAsync(MealFormViewModel mealFormViewModel)
        {
            Meal meal = new Meal()
            {
                Name = mealFormViewModel.Name,
                Description = mealFormViewModel.Description,
                Price = mealFormViewModel.Price,
                ImageUrl = mealFormViewModel.ImageUrl
            };

            await this.dBContext.Meals.AddAsync(meal);
            await this.dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MealAllViewModel>> GetAllMealsAsync()
        {
            var meals = await this.dBContext
                 .Meals
                .Select(m => new MealAllViewModel()
                {
                    Id = m.Id.ToString(),
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price.ToString(),
                    ImageUrl = m.ImageUrl,
                })
                .ToListAsync();

            return meals;
        }
    }
}
