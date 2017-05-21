﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MensariumDesktop.Model.Components;
using MensariumDesktop.Model.Components.DTOs;
using RestSharp;


namespace MensariumDesktop.Model.Controllers
{
    public static class Api
    {
        static string BaseUrl = MSettings.Server.ServerURL + "api/";
        static string SessionID;
        
        private static HttpStatusCode Execute<T>(RestRequest request, out T responseData) where T : new()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            //request.AddParameter("SessionId", SessionID, ParameterType.UrlSegment);
            var response = client.Execute<T>(request);
            
            if (response.ErrorException != null)
            {
                string message = "Greska u komuniciranju sa serverom. Proveri internet konekciju.\n\n" + response.ErrorMessage;
                var Exception = new ApplicationException(message, response.ErrorException);
                throw Exception;
            }

            responseData = response.Data;
            return response.StatusCode;
        }

        private static HttpStatusCode Execute(RestRequest request)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            //request.AddParameter("SessionId", SessionID, ParameterType.UrlSegment);
            var response = client.Execute(request);
            
            if (response.ErrorException != null)
            {
                string message = "Greska u komuniciranju sa serverom. Proveri internet konekciju.\n\n" + response.ErrorMessage;
                var Exception = new ApplicationException(message, response.ErrorException);
                throw Exception;
            }

            return response.StatusCode;
        }

        public static KorisnikFullDto GetUserFullInfo(int id)
        {
            RestRequest request = new RestRequest();
            request.Resource = "korisnici/full/{id}";
            request.AddParameter("id", id, ParameterType.UrlSegment);

            KorisnikFullDto response;
            HttpStatusCode status = Execute<KorisnikFullDto>(request, out response);

            if(status == HttpStatusCode.BadRequest)
                throw new Exception("GetUserFullInfo: Neuspesno pribavljanje podataka o korisniku!");
            return response;
        }
        public static List<KorisnikFullDto> GetUsersFullInfo()
        {
            RestRequest request = new RestRequest();
            request.Resource = "korisnici/full";

            List<KorisnikFullDto> response;
            HttpStatusCode status = Execute<List<KorisnikFullDto>>(request, out response);

            if (status == HttpStatusCode.BadRequest)
                throw new Exception("GetUsersFullInfo: Neuspesno pribavljanje podatak o korisnicima!");

            return response;
        }
        public static HttpStatusCode SendUserFull(KorisnikFullDto user)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.Resource = "korisnici/dodaj";
            request.AddObject(user);

            HttpStatusCode status = Execute(request);
            return status;
        }

        //LOGOVANJE KORISNIKA
        public static SesijaDto LoginUser(ClientLoginDto loginData)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.Resource = "korisnici/prijava";
            request.AddObject(loginData);

            SesijaDto sesija;
            HttpStatusCode status = Execute<SesijaDto>(request, out sesija);

            if (status == HttpStatusCode.BadRequest)
                throw new Exception("LoginUser: Neispravno korisnicko ime ili lozinka");
            return sesija;
        }

        //FAKULTETI
        public static HttpStatusCode AddNewFaculty(FakultetFullDto fax)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.Resource = "fakulteti/dodaj";
            request.AddObject(fax);

            return Execute(request);
        }
    }
}
