using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataConvertHelper<T> where T :new()
    {

       
        #region List 和DataTable
        #region DataTable转成List集合 方法一
        /// <summary>
        /// DataTable转成List集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> DataTableToList(DataTable dt)
        {
            try
            {
                string.IsNullOrWhiteSpace();
                IList<T> ts = new List<T>();// 定义集合
                string tempName = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;
                        if (dt.Columns.Contains(tempName))
                        {
                            if (!pi.CanWrite) continue;
                            object value = dr[tempName];
                            if (value != DBNull.Value)
                                pi.SetValue(t, value, null);
                        }
                    }
                    ts.Add(t);
                }
                return ts;
            }
            catch (Exception ex)
            {
                throw new Exception($"DataTable转成List集合失败,{ex}");
            }
        }
        /// <summary>
        ///  List转成DataTable
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable(IEnumerable<T> collection)

        {

            var props = typeof(T).GetProperties();

            var dt = new DataTable();

            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            if (collection.Count() > 0)

            {

                for (int i = 0; i < collection.Count(); i++)

                {

                    ArrayList tempList = new ArrayList();

                    foreach (PropertyInfo pi in props)

                    {

                        object obj = pi.GetValue(collection.ElementAt(i), null);

                        tempList.Add(obj);

                    }

                    object[] array = tempList.ToArray();

                    dt.LoadDataRow(array, true);

                }

            }

            return dt;

        } 
        #endregion

        #region DataTable转成List集合 方法二
        /// <summary>  

        /// Convert a List{T} to a DataTable. DataTable转成List集合  推荐

        /// </summary>  

        private DataTable ListToDataTable(List<T> items)

        {

            var tb = new DataTable(typeof(T).Name);



            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);



            foreach (PropertyInfo prop in props)

            {

                Type t = GetCoreType(prop.PropertyType);

                tb.Columns.Add(prop.Name, t);

            }



            foreach (T item in items)

            {

                var values = new object[props.Length];



                for (int i = 0; i < props.Length; i++)

                {

                    values[i] = props[i].GetValue(item, null);

                }



                tb.Rows.Add(values);

            }



            return tb;

        }
        /// <summary>  

        /// Determine of specified type is nullable  

        /// </summary>  
        public static bool IsNullable(Type t)

        {

            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));

        }
        /// <summary>  

        /// Return underlying type if type is Nullable otherwise return the type  

        /// </summary>  

        public static Type GetCoreType(Type t)

        {

            if (t != null && IsNullable(t))

            {

                if (!t.IsValueType)

                {

                    return t;

                }

                else

                {

                    return Nullable.GetUnderlyingType(t);

                }

            }

            else

            {

                return t;

            }

        } 
        #endregion
        #endregion
    }



    public class BaseController : ApiController
    {
        public int loginid { get; set; }

        public string loginname { get; set; }


        public BaseBll baseBll { get; set; }


        protected override void Initialize(HttpControllerContext controllerContext)
        {
            //初始化请求上下文
            base.Initialize(controllerContext);
            try
            {
                new SortedDictionary<string, string>();
                string username = string.Empty;
                HttpRequestHeaders headers = controllerContext.Request.Headers;
                if (headers.Contains("e"))
                {
                    text = (headers.GetValues("e").FirstOrDefault<string>().ToString() ?? string.Empty);
                    text = System.Web.HttpUtility.UrlDecode(username);
                }
                UserInfoEntity userInfo = new LoginBll().GetUserInfo(username);
                this.loginid = userInfo.LoginID;
                this.loginname = userInfo.LoginName;
                List<UserAuthorityEntity> tempList = userInfo.UserRole.UserAuthority;
                //不存在安全问题 后续文章有权限验证
                if (tempList.Where(c => c.AuthorityName == "权限名称").ToList().Count > 0)
                {   
                    //调用一个有权限的bll层
                    this.baseBll = new SeniorBll();
                }
                else
                { 
                    //调用一个没有权限的bll层
                    this.baseBll = new OrdinaryBll();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog("Initialize", ex);
            }
        }
        /// <summary>
        /// 设置action返回信息
        /// </summary>
        /// <param name="result">返回实体</param>
        /// <returns></returns>
        protected HttpResponseMessage GetHttpResponseMessage(object result)
        {
            BaseResponseEntity<object> responseBaseEntity = new BaseResponseEntity<object>(0, result, string.Empty);
            return new HttpResponseMessage()
            {
                Content =
                   new StringContent(JsonConvert.SerializeObject(responseBaseEntity, dtConverter), System.Text.Encoding.UTF8,
                       "application/json")
            };
        }
        /// <summary>
        /// 设置action返回信息
        /// </summary>
        /// <param name="result">返回实体</param>
        /// <param name="msg">返回的信息参数</param>
        /// <returns></returns>
        protected HttpResponseMessage GetHttpResponseMessage(object result, ref string msg)
        {
            BaseResponseEntity<object> responseBaseEntity = new BaseResponseEntity<object>(0, result, msg ?? string.Empty);
            return new HttpResponseMessage()
            {
                Content =
                   new StringContent(JsonConvert.SerializeObject(responseBaseEntity, dtConverter), System.Text.Encoding.UTF8,
                       "application/json")
            };
        }
    }
}
