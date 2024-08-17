﻿namespace VNGExercises.Contract.Services.V1.Post
{
    public static class Response
    {
        public record PostResponse(Guid Id, string Title, string Author, DateTime PublishedAt);
    }
}
