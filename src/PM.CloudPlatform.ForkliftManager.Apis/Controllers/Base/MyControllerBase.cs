using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PM.CloudPlatform.ForkliftManager.Apis.DtoParameters.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Entities.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Models.Base;
using PM.CloudPlatform.ForkliftManager.Apis.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PM.CloudPlatform.ForkliftManager.Apis.Controllers.Base
{
    /// <summary>
    /// 基础Api配置
    /// </summary>
    /// <typeparam name="TRepository"> 仓储类型 </typeparam>
    /// <typeparam name="TEntity"> 实体类型 </typeparam>
    /// <typeparam name="TModel"> 模型类型 </typeparam>
    public class MyControllerBase<TRepository, TEntity, TModel> : ControllerBase
        where TRepository : RepositoryBase<TEntity> where TEntity : EntityBase where TModel : DtoBase
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 实例化基础Api配置
        /// </summary>
        /// <param name="repository"> 配置仓储 </param>
        /// <param name="mapper">     映射器 </param>
        public MyControllerBase(RepositoryBase<TEntity> repository, IMapper mapper)
        {
            _repository = (TRepository)(repository ?? throw new ArgumentNullException(nameof(repository)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 获取所有的实体
        /// </summary>
        /// <returns> 所有的实体数据 </returns>
        [HttpGet]
        public async Task<IActionResult> GetEntitiesAsync()
        {
            var data = await _repository.GetEntitiesAsync();

            var returnDto = _mapper.Map<IEnumerable<TModel>>(data);

            return Success(data);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="parameters"> 参数 </param>
        /// <returns> 分页数据 </returns>
        [HttpGet]
        public async Task<IActionResult> GetEntitiesByPagedAsync([FromQuery] DtoParametersBase parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = await _repository.GetEntitiesAsync(parameters);
            var returnDto = _mapper.Map<IEnumerable<TModel>>(data);

            // HACK: UserController 添加Links
            return Success(returnDto);
        }

        /// <summary>
        /// 通过Id获取数据
        /// </summary>
        /// <param name="id"> Id </param>
        /// <returns> 数据 </returns>
        [HttpGet]
        public async Task<IActionResult> GetEntityByIdAsync(Guid id)
        {
            var data = await _repository.GetEntityByIdAsync(id);

            //var returnDto = _mapper.Map<TModel>(data);

            return Success(data);
        }

        /// <summary>
        /// 通过数据集合获取数据
        /// </summary>
        /// <param name="ids"> Id集合 </param>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetEntitiesCollectionAsync(
            [FromQuery] IEnumerable<Guid> ids)
        {
            var entities = await _repository.GetEntitiesByIdsAsync(ids);

            if (ids.Count() != entities.Count())
            {
                return Fail("参数异常");
            }

            var returnDto = _mapper.Map<IEnumerable<TModel>>(entities);

            return Success(returnDto);
        }

        /// <summary>
        /// 创建一条实体数据
        /// </summary>
        /// <param name="entity"> 实体数据(Id不必填写) </param>
        /// <returns> 创建的数据 </returns>
        [HttpPost]
        public async Task<IActionResult> CreateEntityAsync([FromBody] TModel entity)
        {
            if (entity is null)
            {
                return Fail("参数错误");
            }

            var data = _mapper.Map<TEntity>(entity);
            data.Create();

            await _repository.AddEntityAsync(data);
            await _repository.SaveAsync();

            var returnDto = _mapper.Map<TModel>(data);

            return Success(new { data.Id, returnDto });
        }

        /// <summary>
        /// 创建很多条实体数据
        /// </summary>
        /// <param name="models"> 实体数据 </param>
        /// <returns> </returns>
        [HttpPost]
        public async Task<IActionResult> CreateEntitiesAsync([FromBody] IEnumerable<TModel> models)
        {
            if (models is null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var entities = _mapper.Map<IEnumerable<TEntity>>(models);

            foreach (var item in entities)
            {
                item.Create();
            }

            await _repository.AddEntitiesAsync(entities);
            await _repository.SaveAsync();

            var returnDto = _mapper.Map<IEnumerable<TModel>>(entities);

            return Success(entities);
        }

        // TODO: Update需要重新写一下

        /// <summary>
        /// 更新一条实体的数据
        /// </summary>
        /// <param name="model"> 更新的数据 </param>
        /// <returns> 更新后的数据 </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEntityAsync([FromBody] TEntity model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!await _repository.EntityExistAsync(model))
            {
                return Fail("数据不存在");
            }

            var oldEntity = await _repository.GetEntityByIdAsync(model.Id);

            if (oldEntity is null)
            {
                var addEntity = _mapper.Map<TEntity>(model);
                addEntity.Create();
                addEntity.Modify();
                addEntity.Id = model.Id;

                await _repository.AddEntityAsync(addEntity);

                await _repository.SaveAsync();

                var dtoToReturn = _mapper.Map<TModel>(addEntity);

                return Success("创建成功", dtoToReturn);
            }

            _mapper.Map(model, oldEntity);
            oldEntity.Modify();
            await _repository.UpdateEntityAsync(oldEntity);
            await _repository.SaveAsync();

            return Success("更新成功");
        }

        /// <summary>
        /// 更新多条实体数据
        /// </summary>
        /// <param name="models"> 实体数据的集合 </param>
        /// <returns> 更新失败的数据 </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEntitiesAsync([FromBody] IEnumerable<TEntity> models)
        {
            if (models is null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var returnDto = new List<TEntity>();

            foreach (var model in models)
            {
                if (!await _repository.EntityExistAsync(model))
                {
                    returnDto.Add(model);
                    continue;
                }

                var oldEntity = await _repository.GetEntityByIdAsync(model.Id);

                if (oldEntity is null)
                {
                    var addEntity = _mapper.Map<TEntity>(model);
                    addEntity.Create();
                    addEntity.Modify();
                    addEntity.Id = model.Id;

                    await _repository.AddEntityAsync(addEntity);

                    await _repository.SaveAsync();

                    var dtoToReturn = _mapper.Map<TModel>(addEntity);

                    return Success("创建成功", dtoToReturn);
                }

                oldEntity.Modify();
                _mapper.Map(model, oldEntity);

                await _repository.UpdateEntityAsync(oldEntity);
                await _repository.SaveAsync();
            }

            return Success("更新成功", new
            {
                MissingModels = returnDto
            });
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"> 要删除的数据的Id </param>
        /// <returns> 删除的数据 </returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteEntityAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Fail("请求参数错误");
            }

            if (!await _repository.EntityExistByIdAsync(id))
            {
                return Fail("删除的数据不存在");
            }

            await _repository.DeleteEntityByIdAsync(id);
            await _repository.SaveAsync();
            return Success("删除成功");
        }

        /// <summary>
        /// 删除很多条数据
        /// </summary>
        /// <param name="ids"> id集合 </param>
        /// <returns> 204 </returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteEntitiesAsync(IEnumerable<Guid> ids)
        {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var returnDto = new List<Guid>();

            foreach (var id in ids)
            {
                if (!await _repository.EntityExistByIdAsync(id))
                {
                    returnDto.Add(id);
                }
            }

            await _repository.DeleteEntitiesByIdsAsync(ids);
            await _repository.SaveAsync();
            return Success("删除成功", new
            {
                MissingIds = returnDto
            });
        }

        [NonAction]
        public virtual OkObjectResult Success([ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 200,
                data = value
            });
        }

        [NonAction]
        public virtual OkObjectResult Success()
        {
            return new OkObjectResult(new
            {
                code = 200
            });
        }

        [NonAction]
        public virtual OkObjectResult Success(string msg)
        {
            return new OkObjectResult(new
            {
                code = 200,
                msg = msg
            });
        }

        [NonAction]
        public virtual OkObjectResult Success(string msg, [ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 200,
                msg = msg,
                data = value
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail([ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 400,
                data = value
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail()
        {
            return new OkObjectResult(new
            {
                code = 400
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail(string msg)
        {
            return new OkObjectResult(new
            {
                code = 400,
                msg = msg
            });
        }

        [NonAction]
        public virtual OkObjectResult Fail(string msg, [ActionResultObjectValue] object value)
        {
            return new OkObjectResult(new
            {
                code = 400,
                msg = msg,
                data = value
            });
        }
    }
}