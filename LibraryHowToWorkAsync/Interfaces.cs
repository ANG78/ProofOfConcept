﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowToWorkAsync
{
    public interface IStrategyProcesarInforme
    {
        void Ejecutar(Report informe);
        void Volcar(Report informe, string path);
    }

    public class Report
    {
        public string Title { get; set; }
        public string Results { get; set; }
        public IEnumerable<Serie> Series { get; set; }

    }

    public class PointSerie
    {
        public string IdSerie { get; set; }
        public string IdSerieAndIdThread { get { return IdSerie + " " + IdHilo; } }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime When { get; set; }
        public int IdHilo { get; set; }
    }

    public class Serie
    {
        public string IdSerie { get; set; }
        public int IdSerieY { get; set; }
        public bool IsTime { get; set; } = false;
        public List<PointSerie> Points { get; private set; } = new List<PointSerie>();

        public long TiempoEntreInicioYFinEnMilisegundos()
        {
            var min = Points.Select(x => x.When).Min();
            var max = Points.Select(x => x.When).Max();
            long elapsedTicks = max.Ticks - min.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return (long)elapsedSpan.TotalMilliseconds;
        }
    }

    public delegate void EventNextMethodWasChanged(ETypeImpl newType);
   

    public interface IUseMethod
    {
        IUseMethod Next { get; set; }
        ETypeWork TypeWork { get; set; }
        int NumSteps { get; set; }
        int Level { get; set; }
        ECallNext CallNext { get; set; }
        ETypeImpl TypeImplementation { get; set; }
        EventNextMethodWasChanged EventChange { get; set; }
        String IdMethod { get; }
        string ValidateConfigurations();

    }


    public interface IGenerateSerie
    {
        string Generate(string metod, int i, bool esTiempo = false);
        Report GenateReportWithData();
    }

    public interface IGetBase
    {
    }

    public interface IGetLevel
    {
        uint Level { get; set; }

    }

    public interface IGetId
    {
        string Ident();
    }

    public interface IGetString : IGetBase
    {
        string Main();
    }

    public interface IGetStringAsync : IGetBase
    {
        Task<string> MainAsync();
    }

    public interface IStrategyTodo
    {
        string Todo(string cadena);
        bool IsTime();
        int AmountOfStepsOrMls();
    }

    public enum ETypeWork
    {
        LOOPING,
        SLEEPING
    }





    public static class ETypeWorkExtension
    {
        public static int Factor(this ETypeWork t)
        {
            return t == ETypeWork.LOOPING ? 1 : 10;
        }

        public static string Unit(this ETypeWork t)
        {
            return t == ETypeWork.LOOPING ? "steps" : "mls";
        }
    }

    public enum ETypeImpl
    {
        ASYNC,
        SYNC
    }

    public enum ECallNext
    {
        WAIT_FIRST,
        WAIT_AFTER,
        AWAITER_AFTER,
        NOT_WAIT
    }

    public static class ETypeImplExtension
    {

        public static List<ECallNext> HowToBeCalled(this ETypeImpl imp)
        {
            switch (imp)
            {
                case ETypeImpl.SYNC:
                    return (new List<ECallNext>() { ECallNext.WAIT_FIRST, ECallNext.WAIT_AFTER });
                case ETypeImpl.ASYNC:
                    {
                        var result = new List<ECallNext>();
                        foreach (var aux in Enum.GetValues(typeof(ECallNext)))
                        {
                            result.Add((ECallNext)aux);
                        }
                        return result;
                        
                    }
            }

            return null;
        }
    }
}