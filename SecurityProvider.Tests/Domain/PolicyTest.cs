using System;
using SecurityProvider.Domain;
using SecurityProvider.Domain.Entities.Policy;
using Xunit;

namespace SecurityProvider.Tests.Domain;

public class PolicyTest
{
    private static readonly string _name = "s3_write";
    private static readonly string _description = "allow write in s3";

    public static Policy GetPolicy()
    {
        return new Policy(new PolicyRequiredFields() { Name = _name });
    }

    private Policy GetHydratedPolicy()
    {
        var policy = GetPolicy();
        policy.HydrateOptionalFields(new PolicyOptionalFields() { Description = _description });
        return policy;
    }

    private Policy GetDeletedPolicy()
    {
        var policy = GetPolicy();
        policy.Delete();
        return policy;
    }

    [Fact]
    public void Create_Valid()
    {
        DateTime minDate = DateTime.Now;
        var policyRequiredFields = new PolicyRequiredFields() { Name = _name };

        var policy = new Policy(policyRequiredFields);
        DateTime maxDate = DateTime.Now;

        Assert.Equal(_name, policy.Name);
        Assert.IsType<Guid>(policy.Id);
        Assert.IsType<DateTime>(policy.CreatedAt);
        Assert.True(policy.CreatedAt >= minDate);
        Assert.True(policy.CreatedAt <= maxDate);
        Assert.False(policy.Deleted);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_Invalid(string name)
    {
        var policyRequiredFields = new PolicyRequiredFields() { Name = name };
        var action = new Action(() => new Policy(policyRequiredFields));
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void HydrateRequiredFields_SetNewFieldValue()
    {
        string newName = "s3_read";
        var requiredFields = new PolicyRequiredFields() { Name = newName };
        var policy = GetPolicy();
        var policyId = policy.Id;
        var policyCreatedAt = policy.CreatedAt;

        policy.HydrateRequiredFields(requiredFields);

        Assert.Equal(newName, policy.Name);
        Assert.Equal(policyId, policy.Id);
        Assert.Equal(policyCreatedAt, policy.CreatedAt);
        Assert.False(policy.Deleted);
    }

    [Fact]
    public void HydrateRequiredFields_IgnoreFieldNotInformed()
    {
        var requiredFields = new PolicyRequiredFields() { };
        var policy = GetPolicy();
        var policyId = policy.Id;
        var policyCreatedAt = policy.CreatedAt;

        policy.HydrateRequiredFields(requiredFields);

        Assert.Equal(_name, policy.Name);
        Assert.Equal(policyId, policy.Id);
        Assert.Equal(policyCreatedAt, policy.CreatedAt);
        Assert.False(policy.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void HydrateRequiredFields_Invalid(string name)
    {
        var policy = GetPolicy();
        var policyId = policy.Id;
        var policyRequiredFields = new PolicyRequiredFields() { Name = name };

        var action = new Action(() => policy.HydrateRequiredFields(policyRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(policyId, policy.Id);
        Assert.False(policy.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_SetNewFieldValue()
    {
        var optionalFields = new PolicyOptionalFields() { Description = _description };
        var policy = GetPolicy();
        var policyId = policy.Id;

        policy.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, policy.Description);
        Assert.Equal(policyId, policy.Id);
        Assert.False(policy.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_IgnoreFieldNotInformed()
    {
        var optionalFields = new PolicyOptionalFields() { };
        var policy = GetHydratedPolicy();

        policy.HydrateOptionalFields(optionalFields);

        Assert.Equal(_description, policy.Description);
        Assert.False(policy.Deleted);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void HydrateOptionalFields_Invalid(string description)
    {
        var policy = GetPolicy();
        var policyId = policy.Id;
        var policyOptionalFields = new PolicyOptionalFields() { Description = description };

        var action = new Action(() => policy.HydrateOptionalFields(policyOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.Equal(policyId, policy.Id);
        Assert.False(policy.Deleted);
    }

    [Fact]
    public void Delete()
    {
        var policy = GetPolicy();
        policy.Delete();
        Assert.True(policy.Deleted);
    }


    [Fact]
    public void HydrateRequiredFields_Deleted()
    {
        var policy = GetDeletedPolicy();
        var policyRequiredFields = new PolicyRequiredFields() { Name = "s3_read" };

        var action = new Action(() => policy.HydrateRequiredFields(policyRequiredFields));

        Assert.Throws<DomainException>(action);
        Assert.True(policy.Deleted);
    }

    [Fact]
    public void HydrateOptionalFields_Deleted()
    {
        var policy = GetDeletedPolicy();
        var policyOptionalFields = new PolicyOptionalFields() { Description = _description };

        var action = new Action(() => policy.HydrateOptionalFields(policyOptionalFields));

        Assert.Throws<DomainException>(action);
        Assert.True(policy.Deleted);
    }

    [Fact]
    public void Rebuild()
    {
        var savedPolicy = GetPolicy();
        var requiredFields = new PolicyRequiredFields() { Name = savedPolicy.Name };
        var selfGeneratedFields = new PolicySelfGeneratedFields()
        {
            Id = savedPolicy.Id,
            CreatedAt = savedPolicy.CreatedAt,
            Deleted = savedPolicy.Deleted
        };

        var policy = new PolicyBuilder().Rebuild(requiredFields, selfGeneratedFields);

        Assert.Equal(savedPolicy, policy);
    }
}