import React, { Component } from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import { Container } from 'reactstrap';

// components
import { Header, Sidebar, Breadcrumb, Aside, Footer } from 'components';

// containers
import { CrmDashboard, TaskMgt } from 'containers';

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
                  name="CRM Dashboard"
                  component={CrmDashboard}
                />
                <Route path="/crm/tasks" name="CRM Tasks" component={TaskMgt} />
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
