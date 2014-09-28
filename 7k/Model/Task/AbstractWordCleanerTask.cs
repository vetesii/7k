using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model.Task
{
    abstract class AbstractWordCleanerTask : AbstractTask
    {
        // delegate void workInUIThread();

        static protected object oEmpty = System.Reflection.Missing.Value;
        static protected object oMissing = Type.Missing;
        static protected object oTrue = true;

        static public Boolean _selectRangeInWord = true;


        //public void OnDeserialization(Object sender)
        //{
        //    _state = CleanerTaskState.Wait;
        //    ValidateAndCorrectOptions();

        //    // optional fields
        //}

        //protected CleanerTaskState _state;
        //public CleanerTaskState State
        //{
        //    get { return _state; }
        //    set
        //    {
        //        _state = value;
        //        OnPropertyChanged("State");
        //    }
        //}
        //protected CleanerTaskState StateWithDispatcher
        //{
        //    set
        //    {
        //        workInUIThread st = delegate
        //        {
        //            State = value;
        //        };
        //        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, st);

        //    }
        //}

        ///// <summary>
        ///// Már lehet indítani belsőleg feladatot, csak készíteni kell egy példányt, annak a silertRun kapcsolóját bekapcsolni
        ///// majd a feladat kódjában ha kell ehhez kötni a kiírásokat és egyebeket
        ///// + a validateandcorrectoption, getrequiredoptionlist készítésekor ez is figyelembe lett véve
        ///// </summary>
        //public Boolean silentRun = false;

        //protected Boolean _autoStart;
        //public Boolean AutoStart
        //{
        //    get { return _autoStart; }
        //    set
        //    {
        //        _autoStart = value;
        //        OnPropertyChanged("AutoStart");
        //        TaskManager.Instance.wakeUp();
        //    }
        //}

        //protected Boolean AutoStartWithDispatcher
        //{
        //    set
        //    {
        //        workInUIThread st = delegate
        //        {
        //            AutoStart = value;
        //        };
        //        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, st);

        //    }
        //}


        //protected void GeneralInitialization()
        //{
        //    _state = CleanerTaskState.Wait;
        //    _autoStart = true;

        //    CheckOptions = new ObservableCollection<CheckOption>();
        //    SimpleOptions = new ObservableCollection<SimpleOption>();
        //    ListOptions = new ObservableCollection<ListOption>();
        //}

        //abstract public List<Object> getRequiredOptionList();
        ///// <summary>
        ///// Ellenőrzi, hogy a kapott beállítás lista minden eleme ott van a feladat beállításai között.
        ///// Ha nem, akkor az Alapértelmezett beállítások tárolójából lemásolja a megfelelőt.
        ///// </summary>
        ///// <param name="list"></param>
        //protected void ValidateAndCorrectOptions()
        //{
        //    ObservableCollection<CheckOption> newCOList = new ObservableCollection<CheckOption>();
        //    ObservableCollection<SimpleOption> newSOList = new ObservableCollection<SimpleOption>();
        //    ObservableCollection<ListOption> newLOList = new ObservableCollection<ListOption>();

        //    List<Object> allOption = getRequiredOptionList();

        //    foreach (Object opt in allOption)
        //    {
        //        if (opt is CheckOptionType)
        //        {
        //            Boolean correct = true;
        //            foreach (CheckOption co in this._CheckOptions)
        //                if (co.Identifier.Equals((CheckOptionType)opt))
        //                {
        //                    newCOList.Add(co);
        //                    correct = false;
        //                    break;
        //                }
        //            if (correct) newCOList.Add(DefaultOptions.getDefaultOption((CheckOptionType)opt));
        //        }
        //        else if (opt is SimpleOptionType)
        //        {
        //            Boolean correct = true;
        //            foreach (SimpleOption co in this._SimpleOptions)
        //                if (co.Identifier.Equals((SimpleOptionType)opt))
        //                {
        //                    newSOList.Add(co);
        //                    correct = false;
        //                    break;
        //                }
        //            if (correct) newSOList.Add(DefaultOptions.getDefaultOption((SimpleOptionType)opt));
        //        }
        //        if (opt is ListOptionType)
        //        {
        //            Boolean correct = true;
        //            foreach (ListOption co in this._ListOptions)
        //                if (co.Identifier.Equals((ListOptionType)opt))
        //                {
        //                    newLOList.Add(co);
        //                    correct = false;
        //                    break;
        //                }
        //            if (correct) newLOList.Add(DefaultOptions.getDefaultOption((ListOptionType)opt));
        //        }
        //    }

        //    CheckOptions = newCOList;
        //    SimpleOptions = newSOList;
        //    ListOptions = newLOList;
        //}

        //abstract protected void EmbemedStart(Guid id);
        //public void Start(Guid id)
        //{
        //    if (id == null) id = Guid.NewGuid();

        //    MessageWall.Instance.sendMessageToWallWithDispatcher(id, this.Name, MessageWall.WorkingBrush, MessageState.Work);
        //    try
        //    {
        //        this.State = CleanerTaskState.Work;
        //        EmbemedStart(id);
        //        this.AutoStartWithDispatcher = false;
        //        MessageWall.Instance.changeColorAndNullTaskStateAndPercentWithDispatcher(id, MessageWall.FinishBrush);
        //        this.State = CleanerTaskState.Finished;
        //        TaskManager.Instance.deleteTaskWithDispatcherSyncron(this);
        //    }
        //    catch (StopTaskManagerException)
        //    {
        //        terminateTask(id, "(A folyamat a felhasználó által megállítva)");
        //    }
        //    catch (NullReferenceException)
        //    {
        //        terminateTask(id, "(Valószínűleg nincs kiválasztva word dokumentum)");
        //    }
        //    catch (InvalidSelectedDocumentException)
        //    {
        //        terminateTask(id, "(Válassz ki egy Word dokumentumot, amin dolgozol)");
        //    }
        //    catch (ClosedDocumentOrApplicationException)
        //    {
        //        terminateTask(id, "(A kiválasztott dokumentum vagy maga a Word bezárult)");
        //    }
        //    catch (InvalidCastException)
        //    {
        //        terminateTask(id, "(A kiválasztott dokumentum vagy maga a Word bezárult)");
        //    }
        //    catch (UnidentifiedStyleException e)
        //    {
        //        terminateTask(id, "(A dokumentumból hiányzik a " + e.Message + " stílus.)");
        //    }
        //    catch (ThreadAbortException)
        //    {
        //        terminateTask(id, "(Megszakítva)");
        //    }
        //    catch (Exception e)
        //    {
        //        terminateTask(id, "(Ismeretlen hiba történt: " + e.Message + ")");
        //    }
        //}
        //void terminateTask(Guid id, string message)
        //{
        //    if (this.State.Equals(CleanerTaskState.Work))
        //    {
        //        this.StateWithDispatcher = CleanerTaskState.Error;
        //        this.AutoStartWithDispatcher = false;
        //    }

        //    MessageWall.Instance.changeTaskStateWithDispatcher(id, message);
        //    MessageWall.Instance.changeColorWithDispatcher(id, MessageWall.ErrorBrush);
        //}



        protected bool wordFindAndReplace(object mit, object mire)
        {
            object oReplaceAll = WdReplace.wdReplaceAll;
            Range rng = WordProxy.Instance.ActualDocument.Content;

            rng.Find.ClearFormatting();
            rng.Find.Text = (String)mit;
            rng.Find.Replacement.ClearFormatting();
            rng.Find.Replacement.Text = (String)mire;

            return rng.Find.Execute(
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oReplaceAll, // replace all
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing
                 );
        }
        protected bool wordFindAndReplaceWithRegex(object mit, object mire)
        {
            object oReplaceAll = WdReplace.wdReplaceAll;
            Range rng = WordProxy.Instance.ActualDocument.Content;

            rng.Find.ClearFormatting();
            rng.Find.Text = (string)mit;
            rng.Find.Replacement.ClearFormatting();
            rng.Find.Replacement.Text = (string)mire;

            return rng.Find.Execute(
                 ref oMissing, ref oMissing, ref oMissing,
                 ref oTrue, // regexreplace => true
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oReplaceAll,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing
                 );
        }


        //protected bool betuVagySzamVagyHarmasponte(char mi)
        protected bool CharacterOrNumberOrTriplePoint(char ch)
        {
            if (('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z') || ('0' <= ch && ch <= '9')) return true;
            
            if (ch == '\u2026') return true;

            return false;
        }


        //protected String romaiSzamokVisszaallitasaNagyra(char[] tomb)
        //{
        //    String ret = new string(tomb);
        //    String pattern = @"(\s|^)([ivxlcdm]*)([.!?\s])";
        //    //String replacePattern = "$0$1$2";
        //    try
        //    {
        //        //ret = Regex.Replace(ret, pattern, replacePattern);
        //        ret = Regex.Replace(ret, pattern, m => m.ToString().ToUpper(), RegexOptions.IgnoreCase);
        //    }
        //    catch (Exception) { }

        //    return ret;
        //}

        //protected bool letezikeAParagrafus(Paragraph p)
        //{
        //    if (p == null) return false;
        //    return true;
        //}
        //protected void IsStyleExist(string neve)
        //{
        //    bool megvane = false;
        //    foreach (Microsoft.Office.Interop.Word.Style item in WordProxy.Instance.ActualDocument.Styles)
        //    {
        //        if (item.NameLocal.Equals(neve))
        //        {
        //            megvane = true;
        //            break;
        //        }
        //    }
        //    if (!megvane) throw new Exception("A dokumentumba nem importáltad be ezt a stílust:\n\"" + neve + "\"");
        //}

        //protected Boolean searchCheckOptionValue(CheckOptionType id)
        //{
        //    foreach (var item in this.CheckOptions)
        //        if (item.Identifier.Equals(id)) return item.Value;

        //    throw new OptionNotFoundException();
        //}
        //protected string searchSimpleOptionValue(SimpleOptionType id)
        //{
        //    foreach (var item in this.SimpleOptions)
        //        if (item.Identifier.Equals(id)) return item.Value;

        //    throw new OptionNotFoundException();
        //}
        //protected ObservableCollection<string> searchListOptionValue(ListOptionType id)
        //{
        //    foreach (var item in this.ListOptions)
        //        if (item.Identifier.Equals(id)) return item.Value;

        //    throw new OptionNotFoundException();
        //}

    }
}
