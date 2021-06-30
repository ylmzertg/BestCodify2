using AutoMapper;
using BestCodify.Business.Repository.IRepository;
using BestCodify.Common;
using BestCodify.DataAccess.Data;
using BestCodify.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestCodify.Business
{
    public class CourseImageRepository : ICourseImageRepository
    {
        private readonly BestCodifyContext _dbContext;
        private readonly IMapper _mapper;

        public CourseImageRepository(BestCodifyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> CreateCourseImage(CourseImageDto model)
        {
            var image = _mapper.Map<CourseImageDto, CourseImage>(model);
            await _dbContext.CourseImages.AddAsync(image);
            var data = await _dbContext.SaveChangesAsync();
            return new Result<int>(true, ResultConstant.RecordCreateSuccessfully, data);
        }

        public async Task<Result<int>> DeleteCourseImageByImageId(int imageId)
        {
            var image = await _dbContext.CourseImages.FindAsync(imageId);
            _dbContext.CourseImages.Remove(image);
            var data =  await _dbContext.SaveChangesAsync();
            return new Result<int>(true, ResultConstant.RecordCreateSuccessfully, data);
        }
        public async Task<Result<int>> DeleteCourseImageByCourseId(int courseId)
        {
            //Bırden fazla resım olacagı ıcın bu yontem kullanılıyor.
            var imageList = await _dbContext.CourseImages.Where(x => x.CourseId == courseId).ToListAsync();
            _dbContext.CourseImages.RemoveRange(imageList);
            var data =  await _dbContext.SaveChangesAsync();
            return new Result<int>(true, ResultConstant.RecordCreateSuccessfully, data);
        }

        public async Task<IEnumerable<CourseImageDto>> GetCourseImages(int courseId)
        {
            return _mapper.Map<IEnumerable<CourseImage>, IEnumerable<CourseImageDto>>(
            await _dbContext.CourseImages.Where(x => x.CourseId == courseId).ToListAsync());

        }
    }
}
