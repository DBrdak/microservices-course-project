using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery : IRequest<List<OrderVm>>
{
    public GetOrdersListQuery(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
}