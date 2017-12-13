import React, { Component } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { Container } from 'reactstrap';

// components
import { Header, Sidebar, Breadcrumb, Aside, Footer } from 'components';

// containers
import { CrmDashboard, TaskMgt, TaskForm, Restricted } from 'containers';

class CrmLayout extends Component {
  render() {
    return (
      <div className="app">
        <Header />
        <div className="app-body">
          <Sidebar {...this.props} />
          <main className="main">
            <Breadcrumb />
            <Container fluid>
              <Switch>
                <Route
                  path="/crm/dashboard"
                  name="crm-dashboard"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(CrmDashboard)}
                    </Restricted>
                  )}
                />
                <Route
                  path="/crm/tasks"
                  name="crm-tasks"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(TaskMgt)}
                    </Restricted>
                  )}
                />
                <Route
                  exact
                  path="/crm/task-form/:name"
                  name="task-form"
                  render={routeProps => (
                    <Restricted {...routeProps}>
                      {React.createElement(TaskForm)}
                    </Restricted>
                  )}
                />
                <Redirect from="/crm" to="/crm/dashboard" />
              </Switch>
            </Container>
          </main>
          <Aside />
        </div>
        <Footer />
      </div>
    );
  }
}

export default CrmLayout;
