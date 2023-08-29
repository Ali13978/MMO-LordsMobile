using BackendlessAPI.Async;
using BackendlessAPI.Persistence;
using System.Collections.Generic;

namespace BackendlessAPI.Data
{
	public interface IDataStore<T>
	{
		T Save(T entity);

		void Save(T entity, AsyncCallback<T> responder);

		long Remove(T entity);

		void Remove(T entity, AsyncCallback<long> responder);

		T FindFirst();

		T FindFirst(IList<string> relations);

		void FindFirst(AsyncCallback<T> responder);

		void FindFirst(IList<string> relations, AsyncCallback<T> responder);

		T FindLast();

		T FindLast(IList<string> relations);

		void FindLast(AsyncCallback<T> responder);

		void FindLast(IList<string> relations, AsyncCallback<T> responder);

		BackendlessCollection<T> Find();

		BackendlessCollection<T> Find(BackendlessDataQuery dataQueryOptions);

		void Find(AsyncCallback<BackendlessCollection<T>> responder);

		void Find(BackendlessDataQuery dataQueryOptions, AsyncCallback<BackendlessCollection<T>> responder);

		T FindById(string id);

		T FindById(string id, IList<string> relations);

		void FindById(string id, AsyncCallback<T> responder);

		void FindById(string id, IList<string> relations, AsyncCallback<T> responder);

		void LoadRelations(T entity, IList<string> relations);

		void LoadRelations(T entity, IList<string> relations, AsyncCallback<T> responder);
	}
}
