﻿using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Persistence;
using ServiceStack.ServiceHost;

namespace MediaBrowser.Api
{
    /// <summary>
    /// Class GetSimilarTrailers
    /// </summary>
    [Route("/Trailers/{Id}/Similar", "GET")]
    [Api(Description = "Finds movies and trailers similar to a given trailer.")]
    public class GetSimilarTrailers : BaseGetSimilarItems
    {
    }

    /// <summary>
    /// Class TrailersService
    /// </summary>
    public class TrailersService : BaseApiService
    {
        /// <summary>
        /// The _user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The _user data repository
        /// </summary>
        private readonly IUserDataRepository _userDataRepository;
        /// <summary>
        /// The _library manager
        /// </summary>
        private readonly ILibraryManager _libraryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrailersService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="userDataRepository">The user data repository.</param>
        /// <param name="libraryManager">The library manager.</param>
        public TrailersService(IUserManager userManager, IUserDataRepository userDataRepository, ILibraryManager libraryManager)
        {
            _userManager = userManager;
            _userDataRepository = userDataRepository;
            _libraryManager = libraryManager;
        }

        /// <summary>
        /// Gets the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.Object.</returns>
        public object Get(GetSimilarTrailers request)
        {
            var result = SimilarItemsHelper.GetSimilarItems(_userManager,
                _libraryManager,
                _userDataRepository,
                Logger,
                request, item => item is Movie || item is Trailer,
                SimilarItemsHelper.GetSimiliarityScore);

            return ToOptimizedResult(result);
        }
    }
}
