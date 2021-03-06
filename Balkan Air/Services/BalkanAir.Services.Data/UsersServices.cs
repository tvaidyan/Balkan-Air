﻿namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;
    
    public class UsersServices : IUsersServices
    {       
        private readonly IRepository<User> users;
        
        public UsersServices(IRepository<User> users)
        {
            this.users = users;
        }

        public string AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.users.Add(user);
            this.users.SaveChanges();
            
            return user.Id;
        }

        public User GetUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            return this.users.GetById(id);
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_EMAIL);
            }

            return this.users.All()
                .FirstOrDefault(u => u.Email == email);
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public User UpdateUser(string id, User user)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            if (user == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var userToUpdate = this.users.GetById(id);
            
            if (userToUpdate != null)
            {
                userToUpdate.UserSettings.FirstName = user.UserSettings.FirstName;
                userToUpdate.UserSettings.LastName = user.UserSettings.LastName;
                userToUpdate.UserSettings.DateOfBirth = user.UserSettings.DateOfBirth;
                userToUpdate.UserSettings.IdentityDocumentNumber = user.UserSettings.IdentityDocumentNumber;
                userToUpdate.UserSettings.Nationality = user.UserSettings.Nationality;
                userToUpdate.UserSettings.FullAddress = user.UserSettings.FullAddress;
                userToUpdate.UserSettings.ReceiveEmailWhenNewNews = user.UserSettings.ReceiveEmailWhenNewNews;
                userToUpdate.UserSettings.ReceiveEmailWhenNewFlight = user.UserSettings.ReceiveEmailWhenNewFlight;
                userToUpdate.UserSettings.ReceiveNotificationWhenNewNews = user.UserSettings.ReceiveNotificationWhenNewNews;
                userToUpdate.UserSettings.ReceiveNotificationWhenNewFlight = user.UserSettings.ReceiveNotificationWhenNewFlight;
                userToUpdate.IsDeleted = user.IsDeleted;

                if (!userToUpdate.IsDeleted && userToUpdate.DeletedOn != null)
                {
                    userToUpdate.DeletedOn = null;
                }

                this.users.SaveChanges();
            }

            return userToUpdate;
        }

        public User DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            var userToDelete = this.users.GetById(id);

            if (userToDelete != null)
            {
                userToDelete.IsDeleted = true;
                userToDelete.DeletedOn = DateTime.Now;
                this.users.SaveChanges();
            }

            return userToDelete;
        }

        public void Upload(string userId, byte[] image)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            if (image == null || image.Length == 0)
            {
                throw new ArgumentNullException(ErrorMessages.INVALID_IMAGE_TO_UPLOAD);
            }

            var user = this.GetUser(userId);

            if (user != null)
            {
                user.UserSettings.ProfilePicture = image;
                this.users.SaveChanges();
            }
        }

        public void SetLastLogin(string userEmail, DateTime dateTime)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_EMAIL);
            }

            var user = this.GetUserByEmail(userEmail);

            if (user != null)
            {
                user.UserSettings.LastLogin = dateTime;
                this.users.SaveChanges();
            }
        }

        public void SetLastLogout(string userEmail, DateTime dateTime)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_EMAIL);
            }

            var user = this.GetUserByEmail(userEmail);

            if (user != null)
            {
                user.UserSettings.LastLogout = dateTime;
                this.users.SaveChanges();
            }
        }

        public void SetLogoffForUser(string userId, bool logoff)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            var user = this.users.GetById(userId);

            if (user != null)
            {
                user.DoesAdminForcedLogoff = logoff;
                this.users.SaveChanges();
            }
        }
    }
}
