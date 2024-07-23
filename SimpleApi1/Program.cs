using Commons;
using Commons.Extensions;

Boot.Run(args, "SimpleApi1", svcs => {
    svcs.RegisterLogingServices();
});