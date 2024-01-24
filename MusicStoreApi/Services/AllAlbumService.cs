using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicStoreApi.Entities;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Models;
using System.Linq.Expressions;

namespace MusicStoreApi.Services
{
    public class AllAlbumService : IAllAlbumService
    {
        private readonly ArtistDbContext dbContext;
        private readonly IMapper mapper;

        public AllAlbumService(ArtistDbContext dbContext, IMapper mapper, ILogger<ArtistService> logger, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<AlbumDto> GetAll(AlbumQuery searchQuery)
        {
            var albums = dbContext.Albums;
            if (albums.IsNullOrEmpty()) throw new NotFoundException("list of albums is empty");

            var baseQuery = dbContext.Albums
                .Include(a => a.Songs)
                .Where(a => searchQuery.SearchWord == null || a.Title.ToLower().Contains(searchQuery.SearchWord.ToLower()));

            if (!string.IsNullOrEmpty(searchQuery.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Album, object>>>
                {
                    { nameof(Album.Title), a => a.Title }
                };

                var selectedColumn = columnsSelectors[searchQuery.SortBy];

                baseQuery = searchQuery.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var albumsDtos = mapper.Map<List<AlbumDto>>(baseQuery);

            return albumsDtos;
        }


    }
}
