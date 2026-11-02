using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Extensions;

public static class PaginationExtensions
{
    public static async Task<PagedResponseDto<T>> ToPagedResponseAsync<T>(
        this IQueryable<T> query,
        PaginationParams paginationParams)
    {
        var totalRecords = await query.CountAsync();

        var data = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResponseDto<T>
        {
            Data = data,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(
                totalRecords / (double)paginationParams.PageSize)
        };
    }
}