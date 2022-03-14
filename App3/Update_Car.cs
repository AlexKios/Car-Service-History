using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App3
{
    [Activity(Label = "Update Car")]
    public class Update_Car : Activity
    {
        private EditText getCurrentTyres, getKmDriven, getKmLastOilChange;
        private DatePicker getDateLastOilChange;
        private static string currentTyres, kmDriven, kmLastOilChange, dateLastOilChange;
        Button button;
        Database cars = new Database();
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.update_car);

            getCurrentTyres = FindViewById<EditText>(Resource.Id.tyreType);
            getKmDriven = FindViewById<EditText>(Resource.Id.kilometersDriven);
            getKmLastOilChange = FindViewById<EditText>(Resource.Id.lastOilChange);
            getDateLastOilChange = FindViewById<DatePicker>(Resource.Id.datePicker2);
            
            button = FindViewById<Button>(Resource.Id.save);
            button.Click += onClick;
        }

        private async void onClick(Object sender, EventArgs e)
        {
            var table = cars.GetTable();
            var cars1 = table.ToList();
            var currentCar = cars1[Choose_Car.GetId()];
            
            currentTyres = getCurrentTyres.Text;
            kmDriven = getKmDriven.Text;
            kmLastOilChange = getKmLastOilChange.Text;
            var temp = getDateLastOilChange.DateTime.Date.ToString();
            var temp1 = temp.Split(' ');
            dateLastOilChange = temp1[0];
            
            if (currentTyres == "" && kmDriven == "" && kmLastOilChange == "" && dateLastOilChange == "")
            {
                Toast.MakeText(this, "No data entered. Please enter data.", ToastLength.Long).Show();
            }
            else if (currentTyres == "" || kmDriven == "" || kmLastOilChange == "" || dateLastOilChange == "")
            {
                if (kmDriven!="")
                {
                    if (Convert.ToInt32(kmDriven) < Convert.ToInt32(currentCar.Km))
                    {
                        Toast.MakeText(this, "Invalid kilometers. Please enter valid kilometers", ToastLength.Long).Show();
                    }
                    else
                    {
                        currentCar.Km = kmDriven;
                        cars.Update(currentCar);
                        Toast.MakeText(this, "Comfired data", ToastLength.Short).Show();

                        var intent = new Intent(this, typeof(MainActivity));
                        this.StartActivity(intent);
                    }
                }
                else if (currentTyres != "" && kmDriven != "" && kmLastOilChange != "" && dateLastOilChange != "")
                {
                    if (Convert.ToInt32(kmDriven) < Convert.ToInt32(currentCar.Km) && Convert.ToInt32(kmLastOilChange) < Convert.ToInt32(currentCar.KmOfOil))
                    {
                        Toast.MakeText(this, "Invalid kilometers. Please enter valid kilometers", ToastLength.Long).Show();
                    }
                    if (currentTyres.ToLower() != "all season" && currentTyres.ToLower() != "winter" && currentTyres.ToLower() != "summer")
                    {
                        Toast.MakeText(this, "Invalid tyres.", ToastLength.Long).Show();
                    }
                    else
                    {
                        currentCar.Tyre = currentTyres;
                        currentCar.Km = kmDriven;
                        currentCar.KmOfOil = kmLastOilChange;
                        currentCar.DateOfOil = dateLastOilChange;
                        cars.Update(currentCar);
                        Toast.MakeText(this, "Comfired data", ToastLength.Short).Show();
                        var intent = new Intent(this, typeof(MainActivity));
                        this.StartActivity(intent);
                    }
                }
            }
            else if (currentTyres != "" && kmDriven != "" && kmLastOilChange != "" && dateLastOilChange != "")
            {
                if (Convert.ToInt32(kmDriven) < Convert.ToInt32(currentCar.Km) && Convert.ToInt32(kmLastOilChange) < Convert.ToInt32(currentCar.KmOfOil))
                {
                    Toast.MakeText(this, "Invalid kilometers. Please enter valid kilometers", ToastLength.Long).Show();
                }
                if (currentTyres.ToLower() != "all season" && currentTyres.ToLower() != "winter" && currentTyres.ToLower() != "summer")
                {
                    Toast.MakeText(this, "Invalid tyres.", ToastLength.Long).Show();
                }
                else
                {
                    currentCar.Tyre = currentTyres;
                    currentCar.Km = kmDriven;
                    currentCar.KmOfOil = kmLastOilChange;
                    currentCar.DateOfOil = dateLastOilChange;
                    cars.Update(currentCar);
                    Toast.MakeText(this, "Comfired data", ToastLength.Short).Show();
                    var intent = new Intent(this, typeof(MainActivity));
                    this.StartActivity(intent);
                }
            }
        }
    }
}