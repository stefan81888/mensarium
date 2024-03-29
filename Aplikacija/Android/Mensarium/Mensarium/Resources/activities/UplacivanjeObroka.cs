using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Mensarium.Components;

namespace Mensarium.Resources.activities
{
    [Activity(Label = "Uplacivanje obroka")]
    class UplacivanjeObroka : Activity
    {
        private List<string> niz = new List<string>();

        private Spinner spinerDorucak;
        private Spinner spinerRucak;
        private Spinner spinerVecera;

        private int brojDorucka = 0;
        private int brojRucka = 0;
        private int brojVecera = 0;

        private TextView ukupnaCena;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.uplatiObrok);

            //back dugme gore
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            ukupnaCena = FindViewById<TextView>(Resource.Id.ukupnaCena);

            NapuniString();

            //nadjemo spinere
            spinerDorucak = FindViewById<Spinner>(Resource.Id.spinerBrojDorucka);
            spinerRucak = FindViewById<Spinner>(Resource.Id.spinerBrojRucka);
            spinerVecera = FindViewById<Spinner>(Resource.Id.spinerBrojVecera);

            //dodajemo evente
            spinerDorucak.ItemSelected += SpinerDorucakOnItemSelected;
            spinerRucak.ItemSelected += SpinerRucakOnItemSelected;
            spinerVecera.ItemSelected += SpinerVeceraOnItemSelected;

            //adapter
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, niz);
            spinerDorucak.Adapter = adapter;
            spinerRucak.Adapter = adapter;
            spinerVecera.Adapter = adapter;

            //dugmeUplati
            Button dugme = FindViewById<Button>(Resource.Id.uplatiObrokDugme);
            dugme.Click += DugmeOnClick;
        }

        private void SpinerVeceraOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs itemSelectedEventArgs)
        {
            Spinner spiner = (Spinner) sender;
            this.brojVecera = (int)spiner.GetItemIdAtPosition(itemSelectedEventArgs.Position);

            IzracunajUkupnuCenu();
        }

        private void SpinerRucakOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs itemSelectedEventArgs)
        {
            Spinner spiner = (Spinner)sender;
            this.brojRucka = (int)spiner.GetItemIdAtPosition(itemSelectedEventArgs.Position);

            IzracunajUkupnuCenu();
        }

        private void SpinerDorucakOnItemSelected(object sender, AdapterView.ItemSelectedEventArgs itemSelectedEventArgs)
        {
            Spinner spiner = (Spinner)sender;
            this.brojDorucka = (int)spiner.GetItemIdAtPosition(itemSelectedEventArgs.Position);

            IzracunajUkupnuCenu();
        }

        private void NapuniString()
        {
            DateTime dt = DateTime.Today;
            for (int i = 0; i <= DateTime.DaysInMonth(dt.Year, dt.Month) - dt.Day; ++i)
                niz.Add(i.ToString());
        }

        private void IzracunajUkupnuCenu()
        {
            this.ukupnaCena.Text = "Ukupna cena: " + (this.brojDorucka * 40 + this.brojRucka * 72 + this.brojVecera * 59).ToString() + " din";
        }

        private void DugmeOnClick(object sender, EventArgs eventArgs)
        {
            string formatPoruke;
            int broj = 0;

            if (this.brojDorucka != 0)
            {
                formatPoruke = "MENSARIUM " + MSettings.CurrentSession.LoggedUser.UserID + " DORUCAK " +
                               this.brojDorucka.ToString();

                SmsManager.Default.SendTextMessage("062719720", null, formatPoruke, null, null);
                broj++;
            }

            if (this.brojRucka != 0)
            {
                formatPoruke = "MENSARIUM " + MSettings.CurrentSession.LoggedUser.UserID + " RUCAK " +
                               this.brojRucka.ToString();

                SmsManager.Default.SendTextMessage("062719720", null, formatPoruke, null, null);
                broj++;
            }

            if (this.brojVecera != 0)
            {
                formatPoruke = "MENSARIUM " + MSettings.CurrentSession.LoggedUser.UserID + " VECERA " +
                               this.brojVecera.ToString();

                SmsManager.Default.SendTextMessage("062719720", null, formatPoruke, null, null);
                broj++;
            }

            if(broj != 0 && broj == 1)
                Toast.MakeText(this, "SMS za upaltu je poslat. Ocekujte SMS potvrdu uplate!", ToastLength.Long).Show();
            else if (broj != 0 && broj > 1)
                Toast.MakeText(this, "SMS-i za upaltu su poslati. Ocekujte SMS-e radi potvrde uplata!", ToastLength.Long).Show();
        }

        //za back dugme gore
        public override bool OnNavigateUp()
        {
            Finish();
            return true;
        }
    }
}