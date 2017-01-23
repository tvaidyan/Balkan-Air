﻿namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using BalkanAir.Data.Models;
    using Data.Services.Contracts;

    public partial class ManageAircrafts : Page
    {
        [Inject]
        public IAircraftManufacturersServices AircraftManufacturersServices { get; set; }

        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Aircraft> ManageAircraftsGridView_GetData()
        {
            return this.AircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Model);
        }

        public void ManageAircraftsGridView_UpdateItem(int id)
        {
            var aircraft = this.AircraftsServices.GetAircraft(id);

            if (aircraft == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(aircraft);
            if (ModelState.IsValid)
            {
                this.AircraftsServices.UpdateAircraft(id, aircraft);
            }
        }

        public void ManageAircraftsGridView_DeleteItem(int id)
        {
            this.AircraftsServices.DeleteAircraft(id);
        }

        public IQueryable<AircraftManufacturer> AircraftManufacturersDropDownList_GetData()
        {
            return this.AircraftManufacturersServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name);
        }

        protected void CreateAircraftBtn_Click(object sender, EventArgs e)
        {
            var aircraft = new Aircraft()
            {
                Model = this.AircraftModelTextBox.Text.ToUpper(),
                TotalSeats = int.Parse(this.TotalSeatsTextBox.Text),
                AircraftManufacturerId = int.Parse(this.AircraftManufacturersDropDownList.SelectedItem.Value)
            };

            this.AircraftsServices.AddAircraft(aircraft);
        }
    }
}