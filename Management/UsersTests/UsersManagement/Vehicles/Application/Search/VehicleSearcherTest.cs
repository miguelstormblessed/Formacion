using FluentAssertions;
using UsersManagement.Vehicles.Application.Search;
using UsersManagement.Vehicles.Domain;
using UsersTests.UsersManagement.Vehicles.Domain;

namespace UsersTests.UsersManagement.Vehicles.Application.Search;

public class VehicleSearcherTest : VehiclesModuleApplicationUnitTestCase
{
    private readonly VehicleSearcher _searcher;

    public VehicleSearcherTest()
    {
        _searcher = new VehicleSearcher(this._vehicleRepositoryMock.Object);
    }
    [Fact]
    public async Task ShouldCallSearcherWithCorrectParametersOnce()
    {
        // GIVEN
        List<Vehicle> vehicles = new List<Vehicle>
        {
            VehicleMother.CreateRandom()
        };
        await this.ShouldSearchVehicles(vehicles);
        // WHEN
        await this._searcher.Execute();
        // THEN
        await this.ShouldHaveCalledSearchWithCorrectParametersOnce();
    }

    [Fact]
    public async Task ShouldReturnListOfVehicles()
    {
        // GIVEN
        List<Vehicle> vehicles = new List<Vehicle>
        {
            VehicleMother.CreateRandom()
        };
        await this.ShouldSearchVehicles(vehicles);
        // WHEN
        IEnumerable<Vehicle> vehiclesSearched = await this._searcher.Execute();
        // THEN
        vehicles.Should().BeEquivalentTo(vehiclesSearched);
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenVehiclesNotFound()
    {
        // GIVEN
        List<Vehicle> vehicles = new List<Vehicle>(); 
        await this.ShouldSearchVehicles(vehicles);
        // WHEN
        IEnumerable<Vehicle> resutl = await this._searcher.Execute();
        // THEN
        resutl.Should().BeEmpty();
    }
}