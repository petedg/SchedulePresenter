﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class DictionaryValue
    {
        private serverDBEntities context;

        public DictionaryValue(serverDBEntities context)
        {
            this.context = context;
        }

        public DictionaryValue()
        {
            
        }

        public void SetContext(serverDBEntities context)
        {
            this.context = context;
        }

        public string GetValue (string dictionaryName, int dictionaryValueId)
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals(dictionaryName) && dictionaryValue.DV_ID == dictionaryValueId
                                   select dictionaryValue;

            var selectedDictionaryValue = dictionaryValues.FirstOrDefault();            

            return selectedDictionaryValue.VALUE;    
        }

        public int GetId(string dictionaryName, string value)
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals(dictionaryName) && dictionaryValue.VALUE == value
                                   select dictionaryValue;

            var selectedDictionaryValue = dictionaryValues.FirstOrDefault();

            return selectedDictionaryValue.DV_ID;    
        }

        public List<DictionaryValue> GetSemesterTypes()
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals("Typy semestrów")
                                   select dictionaryValue;            

            return dictionaryValues.ToList();
        }

        public List<DictionaryValue> GetMajorDegrees()
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals("Poziomy studiów")
                                   select dictionaryValue;

            return dictionaryValues.ToList();
        }

        public List<DictionaryValue> GetMajorTypes()
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals("Typy studiów")
                                   select dictionaryValue;

            return dictionaryValues.ToList();
        }

        public List<DictionaryValue> GetTeacherDegrees()
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals("Stopnie naukowe nauczycieli")
                                   select dictionaryValue;

            return dictionaryValues.ToList();
        }

        public List<DictionaryValue> GetClassesTypes()
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                               join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                               where dictionary.NAME.Equals("Typy zajęć")
                               select dictionaryValue;

            return dictionaryValues.ToList();
        }

        public string GetTeacherDegree(Teacher teacher)
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals("Stopnie naukowe nauczycieli") && dictionaryValue.DV_ID == teacher.DEGREE_DV_ID
                                   select dictionaryValue.VALUE;

            string teacherDegree = dictionaryValues.FirstOrDefault();

            if (!teacherDegree.Equals("brak"))
            {
                return teacherDegree + " ";
            }

            return "";
        }

        public List<DictionaryValue> GetYearsOfStudy()
        {
            var dictionaryValues = from dictionaryValue in context.DictionaryValue
                                   join dictionary in context.Dictionary on dictionaryValue.DICTIONARY_ID equals dictionary.ID
                                   where dictionary.NAME.Equals("Lata studiów")
                                   select dictionaryValue;

            return dictionaryValues.ToList();
        }

    }
}
