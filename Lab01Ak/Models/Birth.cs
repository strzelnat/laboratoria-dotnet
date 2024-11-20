using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab01Ak.Models;
    public class Birth 
    {
        public string? imie {  get; set; }

        public DateTime? data_ur { get; set; }

        public string ErrorrMessage;

        public bool IsValid()
        {
            if(imie is null)
            {
                ErrorrMessage = "Brakuje parametru imie.";
                return false;
            }

            if (data_ur is null)
            {
                ErrorrMessage = "Brakuje parametru data urodzenia.";
                return false;
            }

            if(data_ur > DateTime.Now)
            {
                ErrorrMessage = "Data urodzin musi być wcześniejsza od daty bieżącej.";
                return false;
            }


            return true;
        }


        public int Birth_fun()
        {
            return DateTime.Now.Year  - data_ur.Value.Year;
        }
    }