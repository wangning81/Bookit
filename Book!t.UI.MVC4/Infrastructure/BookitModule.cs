using Bookit.Biz;
using Bookit.Data;
using Ninject.Modules;

namespace Bookit.UI.Mvc4.Infrastructure
{
    class BookitModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ICalendarFileBuilder>().To<VCalendarBuilder>().InSingletonScope();
#if ONLINEDEBUG
            this.Bind<IMeetingRoomRepository>().To<MeetingRoomDbRepository>().InSingletonScope();
            this.Bind<IRoomFinder>().To<ShortestPathFinder>().InSingletonScope();
#elif OFFLINEDEBUG
            this.Bind<IRoomFinder>().To<DummyFinder>();
#elif RELEASE_ARCA
            this.Bind<IMapRepository>().To<MapDBRepository>().InSingletonScope();
            this.Bind<IMeetingRoomRepository>().To<MeetingRoomFileRepository>().InSingletonScope();
            this.Bind<IRoomFinder>().To<ServiceProxyRoomFinder>().WhenInjectedInto<ShortestPathFinder>();
            this.Bind<IRoomFinder>().To<ShortestPathFinder>().InSingletonScope();
#elif RELEASE_INFO
            this.Bind<IMeetingRoomRepository>().To<MeetingRoomFileRepository>().InSingletonScope();
            this.Bind<IRoomFinder>().To<ServiceProxyRoomFinder>().InSingletonScope();
#endif
        }
    }
}