﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using NUnit.Framework;
using Python.Runtime;
using System;

namespace QuantConnect.Tests.Python
{
    [TestFixture, Category("TravisExclude")]
    public class PythonPackagesTests
    {
        [Test]
        public void NumpyTest()
        {
            AssetCode(
                @"
import numpy
def RunTest():
    return numpy.pi"
            );
        }

        [Test]
        public void ScipyTest()
        {
            AssetCode(
                @"
import scipy
import numpy
def RunTest():
    return scipy.mean(numpy.array([1, 2, 3, 4, 5]))"
            );
        }

        [Test]
        public void SklearnTest()
        {
            AssetCode(
                @"
from sklearn.ensemble import RandomForestClassifier
def RunTest():
    return RandomForestClassifier()"
            );
        }

        [Test]
        public void CvxoptTest()
        {
            AssetCode(
                @"
import cvxopt
def RunTest():
    return cvxopt.matrix([1.0, 2.0, 3.0, 4.0, 5.0, 6.0], (2,3))"
            );
        }

        [Test]
        public void TalibTest()
        {
            AssetCode(
                @"
import numpy
import talib
def RunTest():
    return talib.SMA(numpy.random.random(100))"
            );
        }

        [Test]
        public void BlazeTest()
        {
            AssetCode(
                @"
import blaze
def RunTest():
    accounts = blaze.symbol('accounts', 'var * {id: int, name: string, amount: int}')
    deadbeats = accounts[accounts.amount < 0].name
    L = [[1, 'Alice',   100],
         [2, 'Bob',    -200],
         [3, 'Charlie', 300],
         [4, 'Denis',   400],
         [5, 'Edith',  -500]]
    return blaze.compute(deadbeats, L)"
            );
        }

        [Test]
        public void CvxpyTest()
        {
            AssetCode(
                @"
import numpy
import cvxpy
def RunTest():
    numpy.random.seed(1)
    n = 10
    mu = numpy.abs(numpy.random.randn(n, 1))
    Sigma = numpy.random.randn(n, n)
    Sigma = Sigma.T.dot(Sigma)

    w = cvxpy.Variable(n)
    gamma = cvxpy.Parameter(nonneg=True)
    ret = mu.T*w
    risk = cvxpy.quad_form(w, Sigma)
    return cvxpy.Problem(cvxpy.Maximize(ret - gamma*risk), [cvxpy.sum(w) == 1, w >= 0])"
            );
        }

        [Test]
        public void StatsmodelsTest()
        {
            AssetCode(
                @"
import numpy
import statsmodels.api as sm
def RunTest():
    nsample = 100
    x = numpy.linspace(0, 10, 100)
    X = numpy.column_stack((x, x**2))
    beta = numpy.array([1, 0.1, 10])
    e = numpy.random.normal(size=nsample)

    X = sm.add_constant(X)
    y = numpy.dot(X, beta) + e

    model = sm.OLS(y, X)
    results = model.fit()
    return results.summary()"
            );
        }

        [Test]
        public void PykalmanTest()
        {
            AssetCode(
                @"
import numpy
from pykalman import KalmanFilter
def RunTest():
    kf = KalmanFilter(transition_matrices = [[1, 1], [0, 1]], observation_matrices = [[0.1, 0.5], [-0.3, 0.0]])
    measurements = numpy.asarray([[1,0], [0,0], [0,1]])  # 3 observations
    kf = kf.em(measurements, n_iter=5)
    return kf.filter(measurements)"
            );
        }

        [Test]
        public void CopulalibTest()
        {
            AssetCode(
                @"
import numpy
from copulalib.copulalib import Copula
def RunTest():
    x = numpy.random.normal(size=100)
    y = 2.5 * x + numpy.random.normal(size=100)

    #Make the instance of Copula class with x, y and clayton family::
    return Copula(x, y, family = 'clayton')"
            );
        }

        [Test]
        public void TheanoTest()
        {
            AssetCode(
                @"
import theano
def RunTest():
    a = theano.tensor.vector()          # declare variable
    out = a + a ** 10               # build symbolic expression
    f = theano.function([a], out)   # compile function
    return f([0, 1, 2])"
            );
        }

        [Test]
        public void XgboostTest()
        {
            AssetCode(
                @"
import numpy
import xgboost
def RunTest():
    data = numpy.random.rand(5,10)            # 5 entities, each contains 10 features
    label = numpy.random.randint(2, size=5)   # binary target
    return xgboost.DMatrix( data, label=label)"
            );
        }

        [Test]
        public void ArchTest()
        {
            AssetCode(
                @"
import numpy
from arch import arch_model
def RunTest():
    r = numpy.array([0.945532630498276,
        0.614772790142383,
        0.834417758890680,
        0.862344782601800,
        0.555858715401929,
        0.641058419842652,
        0.720118656981704,
        0.643948007732270,
        0.138790608092353,
        0.279264178231250,
        0.993836948076485,
        0.531967023876420,
        0.964455754192395,
        0.873171802181126,
        0.937828816793698])

    garch11 = arch_model(r, p=1, q=1)
    res = garch11.fit(update_freq=10)
    return res.summary()"
            );
        }

        [Test]
        public void KerasTest()
        {
            AssetCode(
                @"
import numpy
from keras.models import Sequential
from keras.layers import Dense, Activation
def RunTest():
    # Initialize the constructor
    model = Sequential()

    # Add an input layer
    model.add(Dense(12, activation='relu', input_shape=(11,)))

    # Add one hidden layer
    model.add(Dense(8, activation='relu'))

    # Add an output layer
    model.add(Dense(1, activation='sigmoid'))
    return model"
            );
        }

        [Test]
        public void TensorflowTest()
        {
            AssetCode(
                @"
import tensorflow as tf
def RunTest():
    node1 = tf.constant(3.0, tf.float32)
    node2 = tf.constant(4.0) # also tf.float32 implicitly
    sess = tf.Session()
    node3 = tf.add(node1, node2)
    return sess.run(node3)"
            );
        }

        [Test]
        public void DeapTest()
        {
            AssetCode(
                @"
import numpy
from deap import algorithms, base, creator, tools
def RunTest():
    # onemax example evolves to print list of ones: [1, 1, 1, 1, 1, 1, 1, 1, 1, 1]
    numpy.random.seed(1)
    def evalOneMax(individual):
        return sum(individual),

    creator.create('FitnessMax', base.Fitness, weights=(1.0,))
    creator.create('Individual', list, typecode = 'b', fitness = creator.FitnessMax)

    toolbox = base.Toolbox()
    toolbox.register('attr_bool', numpy.random.randint, 0, 1)
    toolbox.register('individual', tools.initRepeat, creator.Individual, toolbox.attr_bool, 10)
    toolbox.register('population', tools.initRepeat, list, toolbox.individual)
    toolbox.register('evaluate', evalOneMax)
    toolbox.register('mate', tools.cxTwoPoint)
    toolbox.register('mutate', tools.mutFlipBit, indpb = 0.05)
    toolbox.register('select', tools.selTournament, tournsize = 3)

    pop = toolbox.population(n = 50)
    hof = tools.HallOfFame(1)
    stats = tools.Statistics(lambda ind: ind.fitness.values)
    stats.register('avg', numpy.mean)
    stats.register('std', numpy.std)
    stats.register('min', numpy.min)
    stats.register('max', numpy.max)

    pop, log = algorithms.eaSimple(pop, toolbox, cxpb = 0.5, mutpb = 0.2, ngen = 30,
                                   stats = stats, halloffame = hof, verbose = False) # change to verbose=True to see evolution table
    return hof[0]"
            );
        }

        [Test]
        public void QuantlibTest()
        {
            AssetCode(
                @"
import QuantLib as ql
def RunTest():
    todaysDate = ql.Date(15, 1, 2015)
    ql.Settings.instance().evaluationDate = todaysDate
    spotDates = [ql.Date(15, 1, 2015), ql.Date(15, 7, 2015), ql.Date(15, 1, 2016)]
    spotRates = [0.0, 0.005, 0.007]
    dayCount = ql.Thirty360()
    calendar = ql.UnitedStates()
    interpolation = ql.Linear()
    compounding = ql.Compounded
    compoundingFrequency = ql.Annual
    spotCurve = ql.ZeroCurve(spotDates, spotRates, dayCount, calendar, interpolation,
                             compounding, compoundingFrequency)
    return ql.YieldTermStructureHandle(spotCurve)"
            );
        }

        [Test]
        public void CopulaTest()
        {
            AssetCode(
                @"
from copulas.univariate.gaussian import GaussianUnivariate
import pandas as pd
def RunTest():
    data=pd.DataFrame({'feature_01': [5.1, 4.9, 4.7, 4.6, 5.0]})
    feature1 = data['feature_01']
    gu = GaussianUnivariate()
    gu.fit(feature1)
    return gu"
            );
        }

        [Test]
        public void HmmlearnTest()
        {
            AssetCode(
                @"
import numpy as np
from hmmlearn import hmm
def RunTest():
    # Build an HMM instance and set parameters
    model = hmm.GaussianHMM(n_components=4, covariance_type='full')
    
    # Instead of fitting it from the data, we directly set the estimated
    # parameters, the means and covariance of the components
    model.startprob_ = np.array([0.6, 0.3, 0.1, 0.0])
    # The transition matrix, note that there are no transitions possible
    # between component 1 and 3
    model.transmat_ = np.array([[0.7, 0.2, 0.0, 0.1],
                               [0.3, 0.5, 0.2, 0.0],
                               [0.0, 0.3, 0.5, 0.2],
                               [0.2, 0.0, 0.2, 0.6]])
    # The means of each component
    model.means_ = np.array([[0.0,  0.0],
                             [0.0, 11.0],
                             [9.0, 10.0],
                             [11.0, -1.0]])
    # The covariance of each component
    model.covars_ = .5 * np.tile(np.identity(2), (4, 1, 1))

    # Generate samples
    return model.sample(500)"
            );
        }

        [Test]
        public void PomegranateTest()
        {
            AssetCode(
                @"
from pomegranate import *
def RunTest():
    d1 = NormalDistribution(5, 2)
    d2 = LogNormalDistribution(1, 0.3)
    d3 = ExponentialDistribution(4)
    d = IndependentComponentsDistribution([d1, d2, d3])
    X = [6.2, 0.4, 0.9]
    return d.log_probability(X)"
            );
        }

        [Test]
        public void LightgbmTest()
        {
            AssetCode(
                @"
import lightgbm as lgb
import numpy as np
import pandas as pd
from scipy.special import expit
def RunTest():
    # Simulate some binary data with a single categorical and
    # single continuous predictor
    np.random.seed(0)
    N = 1000
    X = pd.DataFrame({
        'continuous': range(N),
        'categorical': np.repeat([0, 1, 2, 3, 4], N / 5)
    })
    CATEGORICAL_EFFECTS = [-1, -1, -2, -2, 2]
    LINEAR_TERM = np.array([
        -0.5 + 0.01 * X['continuous'][k]
        + CATEGORICAL_EFFECTS[X['categorical'][k]] for k in range(X.shape[0])
    ]) + np.random.normal(0, 1, X.shape[0])
    TRUE_PROB = expit(LINEAR_TERM)
    Y = np.random.binomial(1, TRUE_PROB, size=N)
    
    return {
        'X': X,
        'probability_labels': TRUE_PROB,
        'binary_labels': Y,
        'lgb_with_binary_labels': lgb.Dataset(X, Y),
        'lgb_with_probability_labels': lgb.Dataset(X, TRUE_PROB),
        }"
            );
        }

        [Test]
        public void FbProphetTest()
        {
            AssetCode(
                @"
import pandas as pd
from fbprophet import Prophet
def RunTest():
    df=pd.DataFrame({'ds': ['2007-12-10', '2007-12-11', '2007-12-12', '2007-12-13', '2007-12-14'], 'y': [9.590761, 8.519590, 8.183677, 8.072467, 7.893572]})
    m = Prophet()
    m.fit(df)
    future = m.make_future_dataframe(periods=365)
    return m.predict(future)"
            );
        }

        [Test]
        public void FastAiTest()
        {
            AssetCode(
                @"
from fastai.text import *
def RunTest():
    return 'Test is only importing the module, since available tests take too long'"
            );
        }

        [Test]
        public void PyramidArimaTest()
        {
            AssetCode(
                @"
import numpy as np
import pyramid as pm
from pyramid.datasets import load_wineind
def RunTest():
    # this is a dataset from R
    wineind = load_wineind().astype(np.float64)

    # fit stepwise auto-ARIMA
    stepwise_fit = pm.auto_arima(wineind, start_p=1, start_q=1,
                                 max_p=3, max_q=3, m=12,
                                 start_P=0, seasonal=True,
                                 d=1, D=1, trace=True,
                                 error_action='ignore',    # don't want to know if an order does not work
                                 suppress_warnings=True,   # don't want convergence warnings
                                 stepwise=True)            # set to stepwise
    
    return stepwise_fit.summary()"
            );
        }

        [Test]
        public void StableBaselinesTest()
        {
            AssetCode(
                @"
from stable_baselines.common.cmd_util import make_atari_env
from stable_baselines import PPO2
def RunTest():
    # There already exists an environment generator that will make and wrap atari environments correctly
    env = make_atari_env('DemonAttackNoFrameskip-v4', num_env=8, seed=0)

    model = PPO2('CnnPolicy', env)
    model.learn(total_timesteps=10)

    obs = env.reset()
    return model.predict(obs)"
            );
        }

        [Test]
        public void WraptTest()
        {
            AssetCode(
                @"
import wrapt

def RunTest():
    assert(wrapt.__version__ == '1.10.11')
    return 'Test passed, module exists'"
            );
        }

        private static void AssetCode(string code)
        {
            using (Py.GIL())
            {
                dynamic module = PythonEngine.ModuleFromString(Guid.NewGuid().ToString(), code);
                Assert.DoesNotThrow(() => module.RunTest());
            }
        }
    }
}