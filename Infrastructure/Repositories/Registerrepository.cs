﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interface.IRepository;
using Dapper;
using Infrastructure.DapperContext;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;
using Domain.Model;


namespace Infrastructure.Repositories
{
  public   class Registerrepository:IRegisterrepository
    {
        private readonly Dappercontext _dappercontext;

        public Registerrepository(Dappercontext dappercontext)
        {
            _dappercontext = dappercontext;
        }
       public async Task<User> AddDonor(DonorRegistrationDto donorRegistrationDto)
{
    var sql = @"
        INSERT INTO users (
            full_name,
            email,
            hashpassword,
            phone_number,
            address,
            city,
            blood_group,
            role_id
          
        ) VALUES (
            @full_name,
            @email,
            @hashpassword,
            @phone_number,
            @address,
            @city,
            @blood_group,
            @role_id
            
        )";

    using var connection = _dappercontext.CreateConnection();

    await connection.ExecuteAsync(sql, donorRegistrationDto);

    var reg = await connection.QueryFirstOrDefaultAsync<User>(
        "SELECT * FROM users WHERE email = @Email",
        new { Email = donorRegistrationDto.email }
    );

    return reg;
}

       public async Task<bool> AddRecipient(RecipientregistrationDto recipientRegistrationDto)
        {
            var sql = @"
        INSERT INTO users (
            full_name,
            email,
            hashpassword,
            phone_number,
            city,
            role_id
           
           
        ) VALUES (
            @full_name,
            @email,
            @hashpassword,
            @phone_number,
            @city,
            @role_id
           
           
        )";

            using var connection = _dappercontext.CreateConnection();

            var result = await connection.ExecuteAsync(sql, recipientRegistrationDto);

            return result > 0;

        }
    public async    Task<User> AddHospital(HospitalregistrationDto hospitalregistrationDto)
        {
            var sql = @"
        INSERT INTO users (
            full_name,
            email,
            hashpassword,
            phone_number,
            address,
            city,
            role_id
         
           
        ) VALUES (
            @full_name,
            @email,
            @hashpassword,
            @phone_number,
            @address,
            @city,
            @role_id
         
           
        )";

           var connection = _dappercontext.CreateConnection();

            var result = await connection.ExecuteAsync(sql, hospitalregistrationDto);

            var reg = await connection.QueryFirstOrDefaultAsync<User>(
       "SELECT * FROM users WHERE email = @Email",
       new { Email = hospitalregistrationDto.email }
              );

            return reg;
  }
        public async Task<User> AddBloodBank(BloodBankRegisterDto bloodBankRegisterDto)
        {
            var sqlUser = @"
        INSERT INTO users (
            full_name, email, hashpassword, phone_number,
            address, city, role_id
        ) VALUES (
            @full_name, @email, @hashpassword, @phone_number,
            @address, @city, @role_id
        );
        SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _dappercontext.CreateConnection();
            connection.Open(); 
            using var transaction = connection.BeginTransaction();

            try
            {
             
                var userId = await connection.ExecuteScalarAsync<int>(sqlUser, bloodBankRegisterDto, transaction);

              
                var sqlBank = @"
            INSERT INTO blood_banks (user_id, bank_name, address)
            VALUES (@UserId, @BankName, @Address);";

                await connection.ExecuteAsync(sqlBank, new
                {
                    UserId = userId,
                    BankName = bloodBankRegisterDto.full_name,
                    Address = bloodBankRegisterDto.address
                }, transaction);

                transaction.Commit();

         
                var reg = await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM users WHERE id = @Id;",
                    new { Id = userId });

                return reg;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error: " + ex.Message);
            }
        }

    }
}
