import React, { Component } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { Container } from 'reactstrap';

import Dashboard from './../Dashboard/Dashboard';

class Full extends Component {
  render() {
    return (
      <div className="app">
        {/* <Header /> */}
        <div className="app-body">
          {/* <Sidebar {...this.props}/> */}
          <main className="main">
            {/* <Breadcrumb /> */}
            <Container fluid>
              <Switch>
                <Route
                  path="/dashboard"
                  name="Dashboard"
                  component={Dashboard}
                />
                <Redirect from="/" to="/dashboard" />
              </Switch>
            </Container>
          </main>
          {/* <Aside /> */}
        </div>
        {/* <Footer /> */}
      </div>
    );
  }
}

export default Full;
