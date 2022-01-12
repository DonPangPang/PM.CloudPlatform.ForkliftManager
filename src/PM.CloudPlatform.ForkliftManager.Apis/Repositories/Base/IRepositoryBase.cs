using PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base
{
    /// <summary>
    /// 仓储基类接口(CRUD)
    /// </summary>
    /// <typeparam name="T">储存的实体类型</typeparam>
    public interface IRepositoryBase<T> where T : EntityBase
    {
        #region 增加 -- C

        /// <summary>
        /// 增加一个Entity
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        void AddEntity(T entity);

        /// <summary>
        /// 增加一个Entity
        /// <remarks>异步</remarks>
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        /// <returns></returns>
        Task AddEntityAsync(T entity);

        /// <summary>
        /// 增加一个Entity集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void AddEntities(IEnumerable<T> entities);

        /// <summary>
        /// 增加一个实体集合
        /// <remarks>异步</remarks>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddEntitiesAsync(IEnumerable<T> entities);

        #endregion 增加 -- C

        #region 查询 -- R

        /// <summary>
        /// 通过Id获取一个实体
        /// </summary>
        /// <param name="id">实体的Id</param>
        /// <returns>实体对象</returns>
        T GetEntityById(Guid id);

        /// <summary>
        /// 通过Id获取一个实体
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="id">实体的Id</param>
        /// <returns>实体对象</returns>
        Task<T> GetEntityByIdAsync(Guid id);

        /// <summary>
        /// 通过Id集合获取多个实体
        /// </summary>
        /// <param name="ids">实体的Id集合</param>
        /// <returns>多个实体</returns>
        IEnumerable<T> GetEntitiesByIds(IEnumerable<Guid> ids);

        /// <summary>
        /// 通过Id集合获取多个实体
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="ids">实体的Id集合</param>
        /// <returns>多个实体</returns>
        Task<IEnumerable<T>> GetEntitiesByIdsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>所有实体的集合</returns>
        IEnumerable<T> GetEntities();

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <remarks>异步</remarks>
        /// <returns>所有实体的集合</returns>
        Task<IEnumerable<T>> GetEntitiesAsync();

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<PagedList<T>> GetEntities(DtoParametersBase parameters);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<PagedList<T>> GetEntitiesAsync(DtoParametersBase parameters);

        #endregion 查询 -- R

        #region 更新 -- U

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <param name="entity">要更新的实体对象</param>
        void UpdateEntity(T entity);

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="entity">要更新的实体对象</param>
        /// <returns></returns>
        Task UpdateEntityAsync(T entity);

        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void UpdateEntities(IEnumerable<T> entities);

        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns></returns>
        Task UpdateEntitiesAsync(IEnumerable<T> entities);

        #endregion 更新 -- U

        #region 删除 -- D

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void DeleteEntity(T entity);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        Task DeleteEntityAsync(T entity);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="id">要删除的实体的Id</param>
        void DeleteEntityById(Guid id);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="id">要删除的实体的Id</param>
        /// <returns></returns>
        Task DeleteEntityByIdAsync(Guid id);

        /// <summary>
        /// 删除一个实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void DeleteEntities(IEnumerable<T> entities);

        /// <summary>
        /// 删除一个实体集合
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="entities">实体集合</param>
        /// <returns></returns>
        Task DeleteEntitiesAsync(IEnumerable<T> entities);

        /// <summary>
        /// 删除一个实体集合
        /// </summary>
        /// <param name="ids">实体的Id集合</param>
        void DeleteEntitiesByIds(IEnumerable<Guid> ids);

        /// <summary>
        /// 删除一个实体集合
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="ids">实体的Id集合</param>
        /// <returns></returns>
        Task DeleteEntitiesByIdsAsync(IEnumerable<Guid> ids);

        #endregion 删除 -- D

        /// <summary>
        /// 通过Id查看一个实体是否存在
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>
        /// <para>true:存在</para>
        /// <para>false:不存在</para>
        /// </returns>
        bool EntityExistById(Guid id);

        /// <summary>
        /// 通过Id查看一个实体是否存在
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="id">Id</param>
        /// <returns>
        /// <para>true:存在</para>
        /// <para>false:不存在</para>
        /// </returns>
        Task<bool> EntityExistByIdAsync(Guid id);

        /// <summary>
        /// 查看一个实体是否存在
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>
        /// <para>true:存在</para>
        /// <para>false:不存在</para>
        /// </returns>
        bool EntityExist(T entity);

        /// <summary>
        /// 查看一个实体是否存在
        /// </summary>
        /// <remarks>异步</remarks>
        /// <param name="entity">实体</param>
        /// <returns>
        /// <para>true:存在</para>
        /// <para>false:不存在</para>
        /// </returns>
        Task<bool> EntityExistAsync(T entity);

        /// <summary>
        /// 保存并提交更改
        /// </summary>
        /// <returns>
        /// <para>true:提交应用成功</para>
        /// <para>false:提交应用失败</para>
        /// </returns>
        bool Save();

        /// <summary>
        /// 保存并提交更改
        /// </summary>
        /// <returns>
        /// <para>true:提交应用成功</para>
        /// <para>false:提交应用失败</para>
        /// </returns>
        Task<bool> SaveAsync();
    }
}