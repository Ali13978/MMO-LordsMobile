using BackendlessAPI.Async;
using BackendlessAPI.Data;
using BackendlessAPI.Engine;
using BackendlessAPI.Exception;
using BackendlessAPI.Persistence;
using BackendlessAPI.Property;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BackendlessAPI.Service
{
	public class PersistenceService
	{
		private const string DEFAULT_OBJECT_ID_PROPERTY_NAME_JAVA_STYLE = "objectId";

		private const string DEFAULT_CREATED_FIELD_NAME_JAVA_STYLE = "created";

		private const string DEFAULT_UPDATED_FIELD_NAME_JAVA_STYLE = "updated";

		private const string DEFAULT_OBJECT_ID_PROPERTY_NAME_DOTNET_STYLE = "ObjectId";

		private const string DEFAULT_CREATED_FIELD_NAME_DOTNET_STYLE = "Created";

		private const string DEFAULT_UPDATED_FIELD_NAME_DOTNET_STYLE = "Updated";

		public const string LOAD_ALL_RELATIONS = "*";

		private static string DELETION_TIME = "deletionTime";

		public T Save<T>(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			CheckEntityStructure<T>();
			return (GetEntityId(entity) != null) ? Update(entity) : Create(entity);
		}

		public void Save<T>(T entity, AsyncCallback<T> callback)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			CheckEntityStructure<T>();
			if (GetEntityId(entity) == null)
			{
				Create(entity, callback);
			}
			else
			{
				Update(entity, callback);
			}
		}

		internal T Create<T>(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			return Invoker.InvokeSync<T>(Invoker.Api.PERSISTENCESERVICE_CREATE, new object[2]
			{
				entity,
				GetTypeName(typeof(T))
			});
		}

		internal void Create<T>(T entity, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_CREATE, new object[2]
			{
				entity,
				GetTypeName(typeof(T))
			}, callback);
		}

		internal T Update<T>(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			return Invoker.InvokeSync<T>(Invoker.Api.PERSISTENCESERVICE_UPDATE, new object[3]
			{
				entity,
				GetTypeName(typeof(T)),
				GetEntityId(entity)
			});
		}

		internal void Update<T>(T entity, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_UPDATE, new object[3]
			{
				entity,
				GetTypeName(typeof(T)),
				GetEntityId(entity)
			}, callback);
		}

		internal long Remove<T>(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			string entityId = GetEntityId(entity);
			if (string.IsNullOrEmpty(entityId))
			{
				throw new ArgumentNullException("Id cannot be null or empty.");
			}
			long result = 0L;
			Dictionary<string, object> dictionary = Invoker.InvokeSync<Dictionary<string, object>>(Invoker.Api.PERSISTENCESERVICE_REMOVE, new object[3]
			{
				null,
				GetTypeName(typeof(T)),
				entityId
			});
			try
			{
				result = (long)dictionary[DELETION_TIME];
				return result;
			}
			catch (System.Exception)
			{
				return result;
			}
		}

		internal void Remove<T>(T entity, AsyncCallback<long> callback)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			string entityId = GetEntityId(entity);
			if (string.IsNullOrEmpty(entityId))
			{
				throw new ArgumentNullException("Id cannot be null or empty.");
			}
			AsyncCallback<Dictionary<string, object>> callback2 = new AsyncCallback<Dictionary<string, object>>(delegate(Dictionary<string, object> r)
			{
				long response = 0L;
				try
				{
					response = (long)r[DELETION_TIME];
				}
				catch (System.Exception)
				{
				}
				if (callback != null)
				{
					callback.ResponseHandler(response);
				}
			}, delegate(BackendlessFault f)
			{
				if (callback != null)
				{
					callback.ErrorHandler(f);
					return;
				}
				throw new BackendlessException(f);
			});
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_REMOVE, new object[3]
			{
				null,
				GetTypeName(typeof(T)),
				entityId
			}, callback2);
		}

		internal T FindById<T>(string id, IList<string> relations)
		{
			if (id == null)
			{
				throw new ArgumentNullException("Id cannot be null or empty.");
			}
			return Invoker.InvokeSync<T>(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				id,
				GetLoadRelationsQuery(relations)
			});
		}

		internal void FindById<T>(string id, IList<string> relations, AsyncCallback<T> callback)
		{
			if (id == null)
			{
				throw new ArgumentNullException("Id cannot be null or empty.");
			}
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				id,
				GetLoadRelationsQuery(relations)
			}, callback);
		}

		internal void LoadRelations<T>(T entity, IList<string> relations)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			string entityId = GetEntityId(entity);
			if (entityId == null)
			{
				throw new ArgumentNullException("Id cannot be null or empty.");
			}
			T response = Invoker.InvokeSync<T>(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				entityId,
				GetLoadRelationsQuery(relations)
			});
			LoadRelationsToEntity(entity, response, relations);
		}

		internal void LoadRelations<T>(T entity, IList<string> relations, AsyncCallback<T> callback)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null.");
			}
			string entityId = GetEntityId(entity);
			if (entityId == null)
			{
				throw new ArgumentNullException("Id cannot be null or empty.");
			}
			AsyncCallback<T> callback2 = new AsyncCallback<T>(delegate(T response)
			{
				LoadRelationsToEntity(entity, response, relations);
				if (callback != null)
				{
					callback.ResponseHandler(response);
				}
			}, delegate(BackendlessFault fault)
			{
				if (callback != null)
				{
					callback.ErrorHandler(fault);
				}
			});
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				entityId,
				GetLoadRelationsQuery(relations)
			}, callback2);
		}

		private void LoadRelationsToEntity<T>(T entity, T response, IList<string> relations)
		{
			if (typeof(T).Equals(typeof(BackendlessUser)))
			{
				object obj = entity;
				object obj2 = response;
				BackendlessUser backendlessUser = (BackendlessUser)obj2;
				BackendlessUser backendlessUser2 = (BackendlessUser)obj;
				backendlessUser2.PutProperties(backendlessUser.Properties);
				return;
			}
			FieldInfo[] fields = typeof(T).GetFields();
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				if (relations.Contains("*") || relations.Contains(fieldInfo.Name))
				{
					fieldInfo.SetValue(entity, fieldInfo.GetValue(response));
				}
			}
			PropertyInfo[] properties = typeof(T).GetProperties();
			PropertyInfo[] array2 = properties;
			foreach (PropertyInfo propertyInfo in array2)
			{
				if (relations.Contains("*") || relations.Contains(propertyInfo.Name))
				{
					propertyInfo.SetValue(entity, propertyInfo.GetValue(response, null), null);
				}
			}
		}

		public List<ObjectProperty> Describe(string className)
		{
			if (string.IsNullOrEmpty(className))
			{
				throw new ArgumentNullException("Entity name cannot be null or empty.");
			}
			return Invoker.InvokeSync<List<ObjectProperty>>(Invoker.Api.PERSISTENCESERVICE_DESCRIBE, new object[2]
			{
				null,
				className
			});
		}

		public void Describe(string className, AsyncCallback<List<ObjectProperty>> callback)
		{
			if (string.IsNullOrEmpty(className))
			{
				throw new ArgumentNullException("Entity name cannot be null or empty.");
			}
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_DESCRIBE, new object[2]
			{
				null,
				className
			}, callback);
		}

		public BackendlessCollection<T> Find<T>(BackendlessDataQuery dataQuery)
		{
			CheckPageSizeAndOffset(dataQuery);
			BackendlessCollection<T> backendlessCollection = Invoker.InvokeSync<BackendlessCollection<T>>(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				null,
				GetFindQuery(dataQuery)
			});
			backendlessCollection.Query = dataQuery;
			return backendlessCollection;
		}

		public void Find<T>(BackendlessDataQuery dataQuery, AsyncCallback<BackendlessCollection<T>> callback)
		{
			AsyncCallback<BackendlessCollection<T>> callback2 = new AsyncCallback<BackendlessCollection<T>>(delegate(BackendlessCollection<T> r)
			{
				r.Query = dataQuery;
				if (callback != null)
				{
					callback.ResponseHandler(r);
				}
			}, delegate(BackendlessFault f)
			{
				if (callback != null)
				{
					callback.ErrorHandler(f);
					return;
				}
				throw new BackendlessException(f);
			});
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				null,
				GetFindQuery(dataQuery)
			}, callback2);
		}

		public T First<T>()
		{
			return First<T>((IList<string>)null);
		}

		public T First<T>(IList<string> relations)
		{
			return Invoker.InvokeSync<T>(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				"first",
				GetLoadRelationsQuery(relations)
			});
		}

		public void First<T>(AsyncCallback<T> callback)
		{
			First(null, callback);
		}

		public void First<T>(IList<string> relations, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				"first",
				GetLoadRelationsQuery(relations)
			}, callback);
		}

		public T Last<T>()
		{
			return Last<T>((IList<string>)null);
		}

		public T Last<T>(IList<string> relations)
		{
			return Invoker.InvokeSync<T>(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				"last",
				GetLoadRelationsQuery(relations)
			});
		}

		public void Last<T>(AsyncCallback<T> callback)
		{
			Last(null, callback);
		}

		public void Last<T>(IList<string> relations, AsyncCallback<T> callback)
		{
			Invoker.InvokeAsync(Invoker.Api.PERSISTENCESERVICE_FIND, new object[4]
			{
				null,
				GetTypeName(typeof(T)),
				"last",
				GetLoadRelationsQuery(relations)
			}, callback);
		}

		public IDataStore<T> Of<T>()
		{
			return DataStoreFactory.CreateDataStore<T>();
		}

		private static string GetEntityId<T>(T entity)
		{
			try
			{
				Type type = entity.GetType();
				if (type.Equals(typeof(BackendlessUser)))
				{
					object obj = entity;
					return ((BackendlessUser)obj).UserId;
				}
				PropertyInfo property = type.GetProperty("ObjectId");
				if (property == null)
				{
					property = type.GetProperty("objectId");
				}
				if (property != null)
				{
					return property.GetValue(entity, null) as string;
				}
				return string.Empty;
				IL_0082:;
			}
			catch (System.Exception)
			{
			}
			return null;
		}

		private static void CheckEntityStructure<T>()
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle.IsArray || typeFromHandle.IsAssignableFrom(typeof(IEnumerable)))
			{
				throw new ArgumentException("Wrong entity type");
			}
			try
			{
				typeFromHandle.GetConstructor(new Type[0]);
			}
			catch (System.Exception)
			{
				throw new ArgumentException("No default noargument constructor");
				IL_0054:;
			}
			PropertyInfo property = typeFromHandle.GetProperty("ObjectId");
			Type type = null;
			if (property == null)
			{
				property = typeFromHandle.GetProperty("objectId");
			}
			if (property != null)
			{
				type = property.PropertyType;
			}
			else
			{
				FieldInfo field = typeFromHandle.GetField("ObjectId");
				if (field == null)
				{
					field = typeFromHandle.GetField("objectId");
				}
				if (field != null)
				{
					type = field.FieldType;
				}
			}
			if (type != null && type != typeof(string))
			{
				throw new ArgumentException("Wrong type of the objectId field");
			}
			Type type2 = null;
			PropertyInfo property2 = typeFromHandle.GetProperty("Created");
			if (property2 == null)
			{
				property2 = typeFromHandle.GetProperty("created");
			}
			if (property2 != null)
			{
				type2 = property2.PropertyType;
			}
			else
			{
				FieldInfo field2 = typeFromHandle.GetField("Created", BindingFlags.Instance | BindingFlags.Public);
				if (field2 == null)
				{
					field2 = typeFromHandle.GetField("created", BindingFlags.Instance | BindingFlags.Public);
				}
				if (field2 != null)
				{
					type2 = field2.FieldType;
				}
			}
			if (type2 != null && type2 != typeof(DateTime) && type2 != typeof(DateTime?))
			{
				throw new ArgumentException("Wrong type of the created field");
			}
			Type type3 = null;
			PropertyInfo property3 = typeFromHandle.GetProperty("Updated");
			if (property3 == null)
			{
				property3 = typeFromHandle.GetProperty("updated");
			}
			if (property3 != null)
			{
				type3 = property3.PropertyType;
			}
			else
			{
				FieldInfo field3 = typeFromHandle.GetField("Updated", BindingFlags.Instance | BindingFlags.Public);
				if (field3 == null)
				{
					field3 = typeFromHandle.GetField("updated", BindingFlags.Instance | BindingFlags.Public);
				}
				if (field3 != null)
				{
					type3 = field3.FieldType;
				}
			}
			if (type3 != null && type3 != typeof(DateTime) && type3 != typeof(DateTime?))
			{
				throw new ArgumentException("Wrong type of the updated field");
			}
		}

		private static void CheckPageSizeAndOffset(IBackendlessQuery dataQuery)
		{
			if (dataQuery != null)
			{
				if (dataQuery.Offset < 0)
				{
					throw new ArgumentException("Offset cannot have a negative value.");
				}
				if (dataQuery.PageSize < 0)
				{
					throw new ArgumentException("Pagesize cannot have a negative value.");
				}
			}
		}

		private static string GetTypeName(Type type)
		{
			if (type.Equals(typeof(BackendlessUser)))
			{
				return "Users";
			}
			return type.Name;
		}

		private static void AddQuery(ref string query, string addQuery)
		{
			if (!string.IsNullOrEmpty(query))
			{
				query += "&";
			}
			query += addQuery;
		}

		private static string GetLoadRelationsQuery(IList<string> relations)
		{
			string result = null;
			if (relations != null)
			{
				string text = string.Empty;
				foreach (string relation in relations)
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += relation;
				}
				if (!string.IsNullOrEmpty(text))
				{
					result = "loadRelations=" + text;
				}
			}
			return result;
		}

		private static string GetFindQuery(BackendlessDataQuery dataQuery)
		{
			string query = null;
			if (dataQuery != null)
			{
				query = string.Empty;
				QueryOptions queryOptions = dataQuery.QueryOptions;
				if (queryOptions != null)
				{
					AddQuery(ref query, "pageSize=" + queryOptions.PageSize);
					AddQuery(ref query, "offset=" + queryOptions.Offset);
					List<string> related = queryOptions.Related;
					if (related != null && related.Count > 0)
					{
						string text = string.Empty;
						foreach (string item in related)
						{
							if (!string.IsNullOrEmpty(text))
							{
								text += ",";
							}
							text += item;
						}
						if (!string.IsNullOrEmpty(text))
						{
							AddQuery(ref query, "loadRelations=" + text);
						}
					}
					List<string> sortBy = queryOptions.SortBy;
					if (sortBy != null && sortBy.Count > 0)
					{
						string text2 = string.Empty;
						foreach (string item2 in sortBy)
						{
							if (!string.IsNullOrEmpty(text2))
							{
								text2 += ",";
							}
							text2 += item2;
						}
						if (!string.IsNullOrEmpty(text2))
						{
							AddQuery(ref query, "sortBy=" + text2);
						}
					}
				}
				string whereClause = dataQuery.WhereClause;
				if (!string.IsNullOrEmpty(whereClause))
				{
					AddQuery(ref query, "where=" + WWW.EscapeURL(whereClause));
				}
				List<string> properties = dataQuery.Properties;
				if (properties != null && properties.Count > 0)
				{
					string text3 = string.Empty;
					foreach (string item3 in properties)
					{
						if (!string.IsNullOrEmpty(text3))
						{
							text3 += ",";
						}
						text3 += item3;
					}
					if (!string.IsNullOrEmpty(text3))
					{
						AddQuery(ref query, "props=" + text3);
					}
				}
			}
			return query;
		}
	}
}
