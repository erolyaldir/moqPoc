using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using MoqPOC.Entities.Base;
using MoqPOC.Repository;
using System.Linq;

namespace MoqPOC
{
    public abstract class RepositoryBaseTest
    {
        public void SetupRepositoryMock<T>(Mock mockRepo, List<T> data) where T : class, IEntityBase
        {
            var mock = mockRepo.As<IRepository<T>>();

            // setup All method
            mock.Setup(x => x.GetAll()).Returns(data.ToList());

            // setup Add method
            mock.Setup(x => x.Add(It.IsAny<T>()))
                .Returns(new Func<T,T>(x =>
                {
                    dynamic lastId = data.Last().Id;
                    dynamic nextId = lastId + 1;
                    x.Id = nextId;
                    data.Add(x);
                    return data.Last();
                }));

            // setup Update method
            mock.Setup(x => x.Update(It.IsAny<T>()))
                .Callback(new Action<T>(x =>
                {
                    var i = data.FindIndex(q => q.Id.Equals(x.Id));
                    data[i] = x;
                }));

            mock.Setup(mr => mr.GetById(It.IsAny<int>())).Returns((int i) => data.Single(x => x.Id == i));

            // setup Delete
            mock.Setup(x => x.Delete(It.IsAny<T>()))
                .Callback(new Action<T>(x =>
                {
                    var i = data.FindIndex(q => q.Id.Equals(x.Id));
                    data.RemoveAt(i);
                }));
        }
    }
}
