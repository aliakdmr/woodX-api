namespace WoodX.API.DTOs;

public record OrderItemInputDto(
    int Id,
    string Name,
    string Image,
    decimal Price,
    int Quantity
);

public record ShippingAddressInputDto(
    string FullName,
    string Email,
    string Address,
    string City,
    string PostalCode,
    string Country,
    string? Phone = null
);

public record CreateOrderDto(
    List<OrderItemInputDto> Items,
    ShippingAddressInputDto ShippingAddress,
    string PaymentMethod,
    decimal Total
);
