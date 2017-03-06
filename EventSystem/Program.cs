using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem
{
  class Caster
  {
    void Invoke(Event aEvent)
    {

    }
  }

  struct CastingCaller<EventType> : Caster
  {
    static void Caster(Event aEvent, Delegate.Caller aCaller)
    {

    }

    public delegate void Caller(Event aEvent);
    Caller mCaller;
  }

  struct Delegate
  {
    public void Invoke(Event aEvent)
    {
      mCaster(aEvent);
    }

    Caster mCaster;
  }

  public class IntrusiveList<TemplateType>
  {
    public class Hook
    {
      ~Hook()
      {
        Unlink();
      }

      public void RemoveFromList()
      {
        mNext.mPrevious = mPrevious;
        mPrevious.mNext = mNext;
      }

      public void Unlink()
      {
        RemoveFromList();

        mPrevious = this;
        mNext = this;
      }

      public void InsertAfter(Hook aHook)
      {
        Unlink();

        mPrevious = aHook;
        mNext = aHook.mNext;
        aHook.mNext = this;

        mNext.mPrevious = this;
      }

      public TemplateType mOwner;
      public Hook mNext;
      public Hook mPrevious;
    }

    public void InsertFront(Hook aHook)
    {
      aHook.InsertAfter(mHead);
    }

    void UnlinkAll()
    {
      for (;;)
      {
        Hook hook = mHead.mPrevious;

        if (hook == mHead)
        {
          break;
        }

        hook.Unlink();
      }
    }

    public Hook mHead;
  }

  public class Event
  {
  }

  public class LogicUpdate : Event
  {
    double mDt;
  }

  public class EventHandler
  {
    
  }

  class Program
  {
    static void Main(string[] args)
    {
    }
  }
}
