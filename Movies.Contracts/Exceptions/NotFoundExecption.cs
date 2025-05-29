using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Exceptions;
public class NotFoundExecption(string message) : Exception(message);