using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem
{
  public class IntrusiveList<TemplateType> : IEnumerable<TemplateType>
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

    IEnumerator<TemplateType> IEnumerable<TemplateType>.GetEnumerator()
    {
      return (IEnumerator<TemplateType>)GetEnumerator();
    }

    public IntrusiveListEnum<TemplateType> GetEnumerator()
    {
      return new IntrusiveListEnum<TemplateType>(mHead);
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

  public class IntrusiveListEnum<TemplateType> : IEnumerator<TemplateType>
  {
    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    IntrusiveList<TemplateType>.Hook mCurrent;
    IntrusiveList<TemplateType>.Hook mHead;

    public IntrusiveListEnum(IntrusiveList<TemplateType>.Hook aHead)
    {
      mHead = aHead;
      mCurrent = mHead.mPrevious;
    }

    public bool MoveNext()
    {
      mCurrent = mCurrent.mNext;
      return true;
    }

    public void Reset()
    {
      mCurrent = mHead.mPrevious;
    }

    //public void Dispose()
    //{
    //
    //}

    object IEnumerator<TemplateType>.Current
    {
      get
      {
        return Current;
      }
    }

    public TemplateType Current
    {
      get
      {
        return mCurrent.mOwner;
      }
    }
  }


  public abstract class Caster
  {
    public abstract void Invoke(Event aEvent);
  }

  class CastingCaller<EventType> : Caster where EventType : Event
  {
    public override void Invoke(Event aEvent)
    {
      mCaller(aEvent as EventType);
    }

    static void Calling(Event aEvent)
    {
      
    }

    public delegate void Caller(EventType aEvent);
    Caller mCaller;
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
    Dictionary<String, List<Caster>> mEventLists;
  }

  class Program
  {
    static void Main(string[] args)
    {
    }
  }
}
