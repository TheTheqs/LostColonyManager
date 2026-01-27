using LostColonyManager.Application.Interfaces;
using LostColonyManager.Application.Mapping;
using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GetStructureUseCase
    {
        private readonly IStructureRepository _repository;

        public GetStructureUseCase(IStructureRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetStructureResponse> ExecuteAsync(
            GetStructureRequest request
        )
        {
            var result = await _repository.GetByIdAsync(request.Id);
            if (result is null)
            {
                var structures = await _repository.GetAllAsync();
                var structureDtos = structures.Select(s => s.ToDto()).ToList();
                return new GetStructureResponse(Structures: structureDtos);
            }
            return new GetStructureResponse(new List<StructureDto>() { result.ToDto() });
        }
    }
}
